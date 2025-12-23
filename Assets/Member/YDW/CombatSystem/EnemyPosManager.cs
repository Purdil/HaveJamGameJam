using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Logger;
using Member.YDW.Agents;
using Member.YDW.AgentSystem;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
using UnityEngine;

namespace Member.YDW.CombatSystem
{
    public class EnemyPosManager : MonoBehaviour
    {
        [SerializeField] private TurnManagerPauseEvent pauseEvent;
        [SerializeField] private Transform[] positions;
        [SerializeField] private EnemyMoveEvent moveEvent;
        
        private Dictionary<Transform, AbstractEnemy> _enemyPosition = new();
        private List<AbstractEnemy> _enemies = new();
        private bool _waitMove;
        public void Initialize(List<AbstractEnemy> enemies)
        {
            _enemies = enemies;
            if (positions.Length < _enemies.Count)
            {
               Logging.Log("아니, 위치가 에너미 수 보다 적잖아 임마, 똑띠 안하냐.");
               return;
            }

            for (int i = 0; i < _enemies.Count; i++)
            {
                _enemyPosition.Add(positions[i],_enemies[i]);
                _enemies[i].transform.position = positions[i].position;
                _enemies[i].Health.OnDeath += DeleteEnemy;
            }

            moveEvent.OnEvent += EnemiesMove;
        }

        private void DeleteEnemy(Agent target)
        {
            _enemies.Remove(target as AbstractEnemy);
            foreach (var positionKey in _enemyPosition.Keys)
            {
                if (_enemyPosition[positionKey] == target)
                {
                    _enemyPosition.Remove(positionKey);
                    break;
                }
            }
            //누가 죽으면 재정렬 시킴.
            _waitMove = true;
            target.Health.OnDeath -= DeleteEnemy;
        }

        private void EnemiesMove(bool _)
        {
            if (!_waitMove)
            {
                pauseEvent.Raise(false);
                return;
            }
            int emptyCount = 0;
            bool reSort = false;
            for (int i = 0; i < positions.Length; i++)
            {
                if (!_enemyPosition.ContainsKey(positions[i])) //비어 있다.
                {
                    emptyCount++;
                    if (i != positions.Length - 1 && _enemyPosition.TryGetValue(positions[i + 1], out AbstractEnemy enemy))
                    {
                        if(emptyCount >= 3 && !reSort) //만약 내 앞 2칸이 비어있다면, 한번 더 정렬시킴.
                            reSort = true;
                        _enemyPosition.Remove(positions[i + 1]);
                        _enemyPosition.Add(positions[i],enemy);
                        enemy.GetCompo<AgentMover>().SetDestination(positions[i].position);
                    }
                }
            }

            if (reSort)
            {
                EnemiesMove(_);
                return;
            }
            Logging.Log("RisePauseFalse");
            StartCoroutine(EndPause()); //정렬이 끝나면 퍼즈 끈냄.
            _waitMove =  false;
        }

        private IEnumerator EndPause()
        {
            yield return new WaitForSeconds(1);
            pauseEvent.Raise(false);
            
        }
    }
}