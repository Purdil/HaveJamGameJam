using System;
using BBJ;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "WishlistDataSO", menuName = "SO/ITEM/WishlistDataSO")]
    public class WishlistDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletOperator, ApplyRouletNum>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        
        public void AfterApply(Func<RouletOperator> Getter, Action<ApplyRouletNum> Setter)
        {
            Logging.Log("0이 나올 확률이 2배 증가함");
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