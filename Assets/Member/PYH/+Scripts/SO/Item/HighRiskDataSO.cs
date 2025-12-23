using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "HighRiskDataSO", menuName = "SO/ITEM/HighRiskDataSO")]
    public class HighRiskDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<ApplyRouletNum>, IAfterApplyTrunIteem<RouletNum, ApplyFinal>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO square;
        
        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum(num2: 0);
            WearOut();
        }
        public void AfterApply(Func<RouletNum> Getter, Action<ApplyFinal> Setter)
        {
            var a = new ApplyFinal(2, square);
            Setter(a);
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