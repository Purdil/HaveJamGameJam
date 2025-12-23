using Core;
using UnityEngine;

namespace Member.YDW.EventStruct
{
    [CreateAssetMenu(fileName = "TurnManagerPause", menuName = "CombatSystem/TurnManagerPauseEvent", order = 0)]
    public class TurnManagerPauseEvent : EventChannel<bool>
    {
        
    }
}