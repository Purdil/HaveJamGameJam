using Core;
using UnityEngine;

namespace Member.YDW.EventChannels
{
    [CreateAssetMenu(fileName = "TryEscape", menuName = "CombatSystem/TryEscapeEvent", order = 0)]
    public class TryEscapeEvent : EventChannel<bool>
    {
        
    }
}