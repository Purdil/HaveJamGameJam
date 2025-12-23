using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class JackpotDataSO : ItemSO, IAfterApplyTrunIteem<RouletNum, RouletOperator>
    {
        [SerializeField] private OperatorSO multiply;
        
        public void AfterApply(Func<RouletNum> Getter, Action<RouletOperator> Setter)
        {
            var a = Getter();
             
            if (a.Num1 == a.Num2 && a.Num1 == a.Num3)
            {
                var b = new RouletOperator(multiply, multiply);
                Setter(b);
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