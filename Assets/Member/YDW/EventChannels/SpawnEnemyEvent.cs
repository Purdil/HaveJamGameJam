using Core;
using Member.YDW.EventStruct;
using UnityEngine;

namespace Member.YDW.EventChannels
{
    [CreateAssetMenu(fileName = "SpawnEnemyEvent", menuName = "CombatSystem/SpawnEnemyEvent", order = 0)]
    public class SpawnEnemyEvent : EventChannel<SpawnEventValue>
    {
        
    }
}