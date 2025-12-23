using System.Collections.Generic;
using Core.Logger;
using Core.PoolSystem;
using Member.PYH._Scripts.Debt;
using Member.YDW.Agents;
using UnityEngine;

namespace Member.YDW.CombatSystem
{
    public class CombatSetter : MonoBehaviour
    {
        [SerializeField] private Transform playerSpawnPos;
        [SerializeField] private PoolableSO playerPool;
        [SerializeField] private PoolableListList enemyLists;
        [SerializeField] private PoolableListList enemyListsInState3;
        [SerializeField] private PoolableListList enemyListsInState6;
        [SerializeField] private PoolableListList enemyListsInState9;
        [SerializeField] private PoolableListList enemyListsInState12;
        public (Player, List<AbstractEnemy>) CreateAgents()
        {
            PoolableListSO enemyList = enemyLists.poolableListSOs[Random.Range(0,enemyLists.poolableListSOs.Count)];
            if(DebtManager.Instance.GetDebted() < 3)
                enemyList = enemyLists.poolableListSOs[Random.Range(0,enemyLists.poolableListSOs.Count)];
            else if(DebtManager.Instance.GetDebted() < 6)
                enemyList = enemyListsInState3.poolableListSOs[Random.Range(0,enemyListsInState3.poolableListSOs.Count)];
            else if(DebtManager.Instance.GetDebted() < 9)
                enemyList = enemyListsInState6.poolableListSOs[Random.Range(0,enemyListsInState6.poolableListSOs.Count)];
            else if(DebtManager.Instance.GetDebted() < 12)
                enemyList = enemyListsInState9.poolableListSOs[Random.Range(0,enemyListsInState9.poolableListSOs.Count)];
            else
                enemyList = enemyListsInState12.poolableListSOs[Random.Range(0,enemyListsInState12.poolableListSOs.Count)];
            
            List<AbstractEnemy> enemies = new List<AbstractEnemy>();
            Player player = PoolManager.Instance.Factory(playerPool).Pop() as Player;
            if(player == null)
                Logging.LogError("플레이어 캐스팅에 실패했습니다. PoolableSO를 점검하세요.");
            else
            {
               player.SettingSO(playerPool);
               player.transform.position = playerSpawnPos.position; 
            }
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