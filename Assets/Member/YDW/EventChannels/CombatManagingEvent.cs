using Core;
using Member.YDW.EventStruct;
using UnityEngine;

namespace Member.YDW.EventChannels
{
    [CreateAssetMenu(fileName = "CombatManagingEvet", menuName = "CombatSystem/CombatManagingEvent", order = 0)]
    public class CombatManagingEvent : EventChannel<CombatManagingEventValue>
    {
        
    }
}