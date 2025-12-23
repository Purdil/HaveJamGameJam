using System;
using BBJ;
using Core.Logger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class WishlistDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletOperator, ApplyRouletNum>
    {
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