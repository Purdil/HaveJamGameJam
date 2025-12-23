using Core;
using Member.YDW.AgentSystem;
using UnityEngine;

namespace Member.YDW.EventChannels
{
    [CreateAssetMenu(fileName = "RandomEvent", menuName = "CombatSystem/RandomEventChannel", order = 0)]
    public class RandomEvent : EventChannel<AgentType> //지금은 AgentType으로 받는데, 추후 룰렛 관련으로 바꿈.
    {
        
    }
}