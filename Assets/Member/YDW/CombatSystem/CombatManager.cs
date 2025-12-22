using System;
using Core.Logger;
using Member.YDW.AgentSystem;
using Member.YDW.EventChannels;
using Member.YDW.EventStruct;
using UnityEngine;

namespace Member.YDW.CombatSystem
{
    public class CombatManager : MonoBehaviour
    {
        [SerializeField] private CombatManagingEvent managingEvent;
        [SerializeField] private CombatSetter setter;
        [SerializeField] private TurnManager turnManager;
       
        
        private void Start()
        {
            
            turnManager.StartCombat(setter.CreateAgents());
            turnManager.StatEvent.OnEvent += HandleNextGame;
        }

        private void HandleNextGame(CombatSettingValue state)
        {
            if (state.State == TurnState.CombatEnd)
            {
                if (state.AgentType == AgentType.Player) //만약 플레이어가 죽었다면,
                {
                    managingEvent.Raise(new CombatManagingEventValue(CombatState.End,AgentType.Enemy));
                    Logging.Log("Enemy Win");
                }

                if (state.AgentType == AgentType.Enemy) //만약 에너미를 다 잡았다면,
                {
                    managingEvent.Raise(new CombatManagingEventValue(CombatState.End,AgentType.Player));
                    Logging.Log("Player Win");
                }
            }
                
            
        }
    }
}