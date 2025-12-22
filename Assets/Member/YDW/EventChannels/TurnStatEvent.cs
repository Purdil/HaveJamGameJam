using Core;
using Member.YDW.EventStruct;
using UnityEngine;

namespace Member.YDW.EventChannels
{
    [CreateAssetMenu(fileName = "TurnStateEvent", menuName = "CombatSystem/TurnStatEvent", order = 0)]
    public class TurnStatEvent : EventChannel<CombatSettingValue>
    {
        
    }
}