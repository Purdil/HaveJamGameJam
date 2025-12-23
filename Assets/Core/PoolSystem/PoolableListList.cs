using System.Collections.Generic;
using UnityEngine;

namespace Core.PoolSystem
{
    [CreateAssetMenu(fileName = "PoolableListList", menuName = "Pool/PoolableListList", order = 0)]
    public class PoolableListList : ScriptableObject
    {
        public List<PoolableListSO> poolableListSOs;
    }
}