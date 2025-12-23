using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "ClowningDataSO", menuName = "SO/ITEM/ClowningDataSO")]
    public class ClowningDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>, IAfterApplyTrunIteem<RouletNum, ApplyFinal>, IWearOutItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO multiply, plus, divide;
        public Action<IDestroyItem> Destroyed { get; set; }
        public int Durability { get; }
        
        public void BeforeApply(Action<RouletOperator> numSetter)
        {
            var a = new RouletOperator();
            int rend1 = Random.Range(1, 4);
            int rend2 = Random.Range(1, 4);
            switch (rend1)
            {
                case 1:
                {
                    a.Operator1 = multiply;
                    break;
                }
                case 2:
                {
                    a.Operator1 = divide;
                    break;
                }
                case 3:
                {
                    a.Operator1 = plus;
                    break;
                }
            }
            switch (rend2)
            {
                case 1:
                {
                    a.Operator2 = multiply;
                    break;
                }
                case 2:
                {
                    a.Operator2 = divide;
                    break;
                }
                case 3:
                {
                    a.Operator2 = plus;
                    break;
                }
            }
            numSetter(a);
        }
        public void AfterApply(Func<RouletNum> Getter, Action<ApplyFinal> Setter)
        {
            var a = new ApplyFinal();
            var b = Getter();
            a.final = b.Num2.Value;
            a.ApplyOperator = plus;
            Setter(a);
            Destroyed?.Invoke(this);
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }
        public void WearOut()
        {
        }
    }
}
