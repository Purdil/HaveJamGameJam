using System.Collections.Generic;
using Core.Logger;
using Core.PoolSystem;
using Member.YDW.Agents;
using UnityEngine;

namespace Member.YDW.CombatSystem
{
    public class CombatSetter : MonoBehaviour
    {
        [SerializeField] private PoolableSO playerPool;
        [SerializeField] private PoolableListSO enemyList;
        
        public (Player, List<AbstractEnemy>) CreateAgents()
        {
            List<AbstractEnemy> enemies = new List<AbstractEnemy>();
            Player player = PoolManager.Instance.Factory(playerPool).Pop() as Player;
            if(player == null)
                Logging.LogError("플레이어 캐스팅에 실패했습니다. PoolableSO를 점검하세요.");
            else
               player.SettingSO(playerPool);
            foreach (var enemy in enemyList.PoolableList)
            {
                AbstractEnemy enemyInstance = PoolManager.Instance.Factory(enemy).Pop() as AbstractEnemy;
                if(enemyInstance == null)
                    Logging.LogError("에너미 캐스팅에 실패했습니다. PoolableSO를 점검하세요.");
                else
                    enemyInstance.SettingSO(enemy);
                enemies.Add(enemyInstance);
            }
            
            return (player, enemies);

        }
    }
}