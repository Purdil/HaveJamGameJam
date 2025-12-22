using Member.YDW.AgentSystem;
using Member.YDW.CombatSystem;

namespace Member.YDW.EventStruct
{
    public struct CombatSettingValue
    {
        public AgentType AgentType;
        public TurnState State;

        public CombatSettingValue(TurnState state, AgentType agentType = AgentType.Empty)
        {
            AgentType  = agentType;
            State = state;
        }
    }
}