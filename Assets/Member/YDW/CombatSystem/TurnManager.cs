using System;
using System.Collections;
using System.Collections.Generic;
using Core.Logger;
using Member.YDW.Agents;
using Member.YDW.AgentSystem;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
using Member.YDW.HealthSystem;
using UnityEngine;

namespace Member.YDW.CombatSystem
{
    public enum TurnState
    {
        Start,
        CombatEnd,
    }
    public class TurnManager : MonoBehaviour
    {
        [field: SerializeField] public TurnStatEvent StatEvent { get; private set; }
        [SerializeField] private TryEscapeEvent _escapeEvent;
        
        
        private List<AbstractEnemy>  _enemies;
        private ITurnAgent _currentTurnAgent;
        private ITurnAgent _player;
        private ITurnAgent _currentEnemy;

        private bool _playerIsEscape;
    

        public void StartCombat((Player,List<AbstractEnemy>) agents)
        {
            _player = agents.Item1;
            _enemies = agents.Item2;
            foreach (var agent in _enemies)
            {
                agent.Health.OnDeath += HandleEnemyDead;
            }
            _playerIsEscape = false;
            _escapeEvent.OnEvent += HandleEscapeEvent;
            StartCoroutine(GameLoop());
        }

        private void HandleEscapeEvent(bool obj)
        {
            Logging.Log($"도망 시도 결과 : {obj}");
            _playerIsEscape = obj;
            if(_playerIsEscape)
                Debug.Log("Sussese Escape");
        }

        private void HandleEnemyDead(Agent agent)
        {
            if (_enemies.Contains(agent as AbstractEnemy))
            {
                _enemies.Remove(agent as AbstractEnemy);
            }

            agent.Health.OnDeath -= HandleEnemyDead;
        }

        private void Update()
        {
            if(_currentTurnAgent != null)
                _currentTurnAgent.Turning();
        }

        private IEnumerator GameLoop()
        {
            StatEvent.Raise(new CombatSettingValue(TurnState.Start));
            _currentEnemy = _enemies[0];
            while (true)
            { 
                _currentTurnAgent = _player;
                if(_currentTurnAgent is Player player)
                    player.InitTargets(_enemies);
                yield return StartCoroutine(StartTurn());
                
                if(_playerIsEscape)
                    break;
                
                if (_currentEnemy.OnDead)
                {
                    Logging.Log("Enemy OnDead");
                    if (_enemies.Count == 0)
                    {
                        StatEvent.Raise(new CombatSettingValue(TurnState.CombatEnd,AgentType.Enemy));
                        break;
                    }
                    _currentEnemy =  _enemies[0];
                }

                _currentTurnAgent = _currentEnemy;
                yield return StartCoroutine(StartTurn());
                
                if(_playerIsEscape)
                    break;
                
                if (_player.OnDead)
                {
                    Logging.Log("Player OnDead");
                    StatEvent.Raise(new CombatSettingValue(TurnState.CombatEnd, AgentType.Player));
                    break;
                }
            }

            GameOver();
        }

        private void GameOver()
        {
            _currentTurnAgent = null;
        }

        private IEnumerator StartTurn()
        {
            if (_currentTurnAgent == null)
            {
                Logging.LogError("현재 선택된 Agent가 존재하지 않습니다.");
                yield break;
            }
            _currentTurnAgent.StartTurn();
            Logging.Log($"{_currentTurnAgent.User.GetInstanceID()} 의 턴이 시작되었습니다.");
            yield return new WaitUntil(() =>
            {
                return _currentTurnAgent.OnTurnEnd || _playerIsEscape;
            });
            _currentTurnAgent.EndTurn();
            
        }

        private void OnDestroy()
        {
            _escapeEvent.OnEvent -= HandleEscapeEvent;
        }
    }
}