using UnityEngine;

namespace Member.YDW.CombatSystem
{
    [CreateAssetMenu(fileName = "RandomEvent", menuName = "CombatSystem/RandomEventSO", order = 0)]
    public class RandomEventSO : ScriptableObject
    {

        [field: SerializeField] public AbstractRandomEvent RandomEvent { get; private set; }
        
    }
}