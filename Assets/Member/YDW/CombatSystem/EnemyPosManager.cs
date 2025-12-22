using System.Collections.Generic;
using Member.YDW.Agents;
using UnityEngine;

namespace Member.YDW.CombatSystem
{
    public class EnemyPosManager : MonoBehaviour
    {
        [SerializeField] private Transform[] _positions;

        private Dictionary<Transform, AbstractEnemy> _enemyPosition;
        private Queue<AbstractEnemy> _waiteEnemies;
        private List<AbstractEnemy> _enemies;

        public void Initialize(List<AbstractEnemy> enemies)
        {
            _enemies = enemies;
            for (int i = 0; i < _positions.Length; i++)
            {
                _enemyPosition.Add(_positions[i],enemies[i]);
            }

            foreach (AbstractEnemy enemy in enemies)
            {
                if (!_enemyPosition.ContainsValue(enemy))
                {
                    _waiteEnemies.Enqueue(enemy);
                }
            }
        }

    }
}