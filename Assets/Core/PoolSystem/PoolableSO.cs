using UnityEngine;

namespace Core.PoolSystem
{
    [CreateAssetMenu(fileName = "Poolable", menuName = "Pool/Poolable", order = 0)]
    public class PoolableSO : ScriptableObject
    {
        [field: SerializeField] public MonoBehaviour Prefab { get; private set; }
        
    }
}