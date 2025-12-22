using System;
using BBJ;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "CrackedFamilyPictureDataSO", menuName = "SO/ITEM/CrackedFamilyPictureDataSO")]
    public class CrackedFamilyPictureDataSO : ItemSO, IAfterApplyTrunIteem<RouletOperator, RouletNum>, IWearOutItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        
        public void AfterApply(Func<RouletOperator> Getter, Action<RouletNum> Setter)
        {
            Logging.Log("Player Health++");
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
