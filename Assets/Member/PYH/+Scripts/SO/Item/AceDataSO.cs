using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "AceDataSO", menuName = "SO/ITEM/AceDataSO")]
    public class AceDataSo : ItemSO, IBeforeApplyTrunItem<ApplyRouletNum>, IProbabilityItem, IWearOutItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO plus;
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        
        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum(num2: 8, applyOperator: plus);
            numSetter(a);
            WearOut();
        }
        public void WearOut()
        {
            Durability--;

            if (Durability == 0)
            {
                Destroyed?.Invoke(this);
            }
        }
        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }
    }
}