using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class HighRiskDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<ApplyRouletNum>, IAfterApplyTrunIteem<ApplyRouletNum, ApplyFinal>
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO square;
        
        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum(num2: 0);
            WearOut();
        }
        public void AfterApply(Func<ApplyRouletNum> Getter, Action<ApplyFinal> Setter)
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