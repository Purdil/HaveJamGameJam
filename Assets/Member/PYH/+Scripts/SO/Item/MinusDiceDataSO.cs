using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "MinusDiceDataSO", menuName = "SO/ITEM/MinusDiceDataSO")]
    public class MinusDiceDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletNum, ApplyRouletNum>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO multiply;
        
        public void AfterApply(Func<RouletNum> Getter, Action<ApplyRouletNum> Setter)
        {
            var a = Getter();
            var b = new ApplyRouletNum(-3, -3, -3, multiply);
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