using Core;
using UnityEngine;

namespace Member.YDW.EventChannels
{
    [CreateAssetMenu(fileName = "EnemyMove", menuName = "CombatSystem/EnemyMove", order = 0)]
    public class EnemyMoveEvent : EventChannel<bool>
    {
        
    }
}