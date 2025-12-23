using System;
using BBJ;
using Core.Logger;
using Member.YDW.EventStruct;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "ComedyMaskDataSO", menuName = "SO/ITEM/ComedyMaskDataSO")]
    public class ComedyMaskDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>
    {
        [field: SerializeField] public float Probability { get; private set; }
        [SerializeField] private EscapeValueEvent _escapeValueEvent;
        
        public void BeforeApply(Action<RouletOperator> numSetter)
        {
            _escapeValueEvent.Raise(100);
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }
    }
}
