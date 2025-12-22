using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "LuckyRouletteDataSO", menuName = "SO/ITEM/LuckyRouletteDataSO")]
    public class LuckyRouletteDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletNum, RouletNum>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        
        public void AfterApply(Func<RouletNum> Getter, Action<RouletNum> Setter)
        {
            var a = Getter();
            var b = new RouletNum(Mathf.Max(a.Num1.GetValueOrDefault(), a.Num2.GetValueOrDefault(), a.Num3.GetValueOrDefault()), Priority: 5);
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