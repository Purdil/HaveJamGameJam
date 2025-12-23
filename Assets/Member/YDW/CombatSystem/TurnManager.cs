using System.Collections;
using System.Collections.Generic;
using Core.Logger;
using Member.YDW.Agents;
using Member.YDW.Agents.Enemys;
using Member.YDW.AgentSystem;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
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
        [SerializeField] private TryEscapeEvent escapeEvent;
        [SerializeField] private TurnManagerPauseEvent pauseEvent;
        [SerializeField] private EnemyMoveEvent enemyMoveEvent;
        [SerializeField] private RandomEvent randomEvent;
        
        private List<AbstractEnemy>  _enemies;
        private ITurnAgent _currentTurnAgent;
        private ITurnAgent _player;
        private ITurnAgent _currentEnemy;

        private bool _playerIsEscape;
        private bool _pause;
        private int _dummyCount = 0;
    

        public void StartCombat((Player,List<AbstractEnemy>) agents)
        {
            _player = agents.Item1;
            _enemies = agents.Item2;
            foreach (var agent in _enemies)
            {
                if(agent is DummyEnemy)
                    _dummyCount++;
                agent.Health.OnDeath += HandleEnemyDead;
            }
            _playerIsEscape = false;
            escapeEvent.OnEvent += HandleEscapeEvent;
            pauseEvent.OnEvent += HandlePauseEvent;
            StartCoroutine(GameLoop());
        }

        private void HandlePauseEvent(bool obj)
        {
            Logging.Log($"Pause Value : {obj}");
            _pause = obj;
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
                Logging.Log(_currentTurnAgent.User.GetInstanceID().ToString());
                if(_currentTurnAgent is Player player)
                    player.InitTargets(_enemies);
                if(_player is ICanHoldRandomEvent agent)
                    randomEvent.Raise(agent);
                yield return StartCoroutine(StartTurn());
                
                while (_pause)
                {
                    yield return null;
                }
                if(_playerIsEscape)
                    break;
                
                if (_currentEnemy.OnDead)
                {
                    Logging.Log("Enemy OnDead");
                    foreach (AbstractEnemy enemy in _enemies)
                    {
                        Logging.Log($"생존한 적 : {enemy.GetInstanceID()}");
                    }
                    if (_enemies.Count == _dummyCount)
                    {
                        StatEvent.Raise(new CombatSettingValue(TurnState.CombatEnd,AgentType.Enemy));
                        break;
                    }
                    _currentEnemy =  _enemies[0];
                }
                
                _pause = true;
                enemyMoveEvent.Raise(true);
                
                while (_pause)
                {
                    yield return null;
                    Logging.Log("Pause 2");
                }
                _currentTurnAgent = _currentEnemy;
                if(_currentEnemy is ICanHoldRandomEvent agent2)
                    randomEvent.Raise(agent2);
                (_currentEnemy as AbstractEnemy)?.InitTarget(_player.User as Player);
                yield return StartCoroutine(StartTurn());
                while (_pause)
                {
                    Logging.Log("Pause 3");
                    yield return null;
                }
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
            yield return new WaitUntil(() => _currentTurnAgent.OnTurnEnd || _playerIsEscape);
            _currentTurnAgent.EndTurn();
            Logging.Log($"End Turn : {_currentTurnAgent.User.GetInstanceID()}");
            _currentTurnAgent = null;
            
        }

        private void OnDestroy()
        {
            escapeEvent.OnEvent -= HandleEscapeEvent;
            pauseEvent.OnEvent -= HandlePauseEvent;
        }
    }
}