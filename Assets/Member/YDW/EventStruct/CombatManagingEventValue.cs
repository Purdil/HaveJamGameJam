using Member.YDW.AgentSystem;

namespace Member.YDW.EventStruct
{
    public enum CombatState
    {
        Start,
        End
    }
    
    public struct CombatManagingEventValue
    {
        public CombatState State;
        public AgentType Winner;

        public CombatManagingEventValue(CombatState state, AgentType winner = AgentType.Empty)
        {
            State = state;
            Winner = winner;
        }
    }
}