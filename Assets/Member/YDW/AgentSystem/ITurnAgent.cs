using System;

namespace Member.YDW.AgentSystem
{
    public interface ITurnAgent
    {
        public Agent User { get; }
        public bool OnTurnEnd {get;}
        public bool OnDead { get; }

        public void StartTurn();

        public void Turning();

        public void EndTurn();
        
        
    }
}