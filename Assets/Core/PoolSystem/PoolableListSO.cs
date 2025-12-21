using System.Collections.Generic;
using UnityEngine;

namespace Core.PoolSystem
{
    [CreateAssetMenu(fileName = "PoolableList", menuName = "Pool/PoolList", order = 0)]
    public class PoolableListSO : ScriptableObject
    {
        [field: SerializeField] public List<PoolableSO> PoolableList { get; private set; }
        
    }
}