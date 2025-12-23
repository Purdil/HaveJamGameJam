using System;
using BBJ;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class LuckyPlusDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<RouletNum>, IProbabilityItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        
        public void BeforeApply(Action<RouletNum> numSetter)
        {
            var a = new RouletNum(num1: 7, num2: 7, num3: 7);
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