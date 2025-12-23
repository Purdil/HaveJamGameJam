using System;
using BBJ;
using Core.Logger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "ComedyMaskDataSO", menuName = "SO/ITEM/ComedyMaskDataSO")]
    public class ComedyMaskDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>
    {
        [field: SerializeField] public float Probability { get; private set; }
        
        public void BeforeApply(Action<RouletOperator> numSetter)
        {
            Logging.Log("RUN!!");
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }

    }
}
