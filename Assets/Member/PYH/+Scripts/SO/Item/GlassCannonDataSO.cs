using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class GlassCannonDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>, IWearOutItem, IAfterApplyTrunIteem<RouletNum, ApplyFinal>
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO multiply, minus;
        
        public void BeforeApply(Action<RouletOperator> operatorSetter)
        {
            var a = new RouletOperator();
            a.Operator1 = multiply;
            a.Operator2 = multiply;
            operatorSetter(a);
        }
        public void AfterApply(Func<RouletNum> Getter, Action<ApplyFinal> Setter)
        {
            var a = Getter;
            var b = new ApplyFinal();
            b.final = 25;
            b.ApplyOperator = minus;
            Setter(b);
            
            int rend = Random.Range(1, 101);
            if (rend <= 25)
            {
                Destroyed?.Invoke(this);
            }
            else
            {
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