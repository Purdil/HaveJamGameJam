using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "JackpotDataSO", menuName = "SO/ITEM/JackpotDataSO")]
    public class JackpotDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletNum, RouletOperator>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO multiply;
        
        public void AfterApply(Func<RouletNum> Getter, Action<RouletOperator> Setter)
        {
            var a = Getter();
             
            if (a.Num1 == a.Num2 && a.Num1 == a.Num3)
            {
                var b = new RouletOperator(multiply, multiply);
                Setter(b);
                WearOut();
            }
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