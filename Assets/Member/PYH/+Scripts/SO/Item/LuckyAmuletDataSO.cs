using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "LuckyAmuletDataSO", menuName = "SO/ITEM/LuckyAmuletDataSO", order = 0)]
    public class LuckyAmuletDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<ApplyRouletNum>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        
        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum(num2: 7);
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