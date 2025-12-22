using System;
using BBJ;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class SevenDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<RouletNum>
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO multiply;
        
        public void BeforeApply(Action<RouletNum> numSetter)
        {
            Logging.Log("룰렛 획득");
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