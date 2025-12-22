using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class HalfBeanDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<RouletOperator>
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
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