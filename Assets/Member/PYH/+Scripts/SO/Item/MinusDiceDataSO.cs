using System;
using BBJ;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class MinusDiceDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletNum, ApplyRouletNum>
    {
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