using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "GrabberDataSO", menuName = "SO/ITEM/GrabberDataSO")]
    public class GrabberDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletNum, ApplyFinal>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO multiply;
        
        public void AfterApply(Func<RouletNum> Getter, Action<ApplyFinal> Setter)
        {
            var a = Getter;
            var b = new ApplyFinal(final: 2, applyOperator: multiply);
            Setter(b);
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