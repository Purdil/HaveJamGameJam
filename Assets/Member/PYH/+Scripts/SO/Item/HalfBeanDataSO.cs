using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "HalfBeanDataSO", menuName = "SO/ITEM/HalfBeanDataSO")]
    public class HalfBeanDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>
    {
        [field: SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO multiply, divide;
        
        public void BeforeApply(Action<RouletOperator> operatorSetter)
        {
            var a = new RouletOperator();
            int rend1 = Random.Range(0, 2);
            int rend2 = Random.Range(0, 2);
            
            if (rend1 > 0)
            {
                if (rend2 > 0)
                {
                    a.Operator1 = multiply;
                }
                else
                {
                    a.Operator1 = divide;
                }
            }
            else
            {
                if (rend2 > 0)
                {
                    a.Operator2 = multiply;
                }
                else
                {
                    a.Operator2 = divide;
                }
            }
            operatorSetter(a);
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }
    }
}