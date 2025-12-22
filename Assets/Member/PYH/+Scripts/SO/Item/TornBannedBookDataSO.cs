using System;
using BBJ;
using Core.Logger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class TornBannedBookDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<RouletOperator>
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO plus, minus;
        
        public void BeforeApply(Action<RouletOperator> operatorSetter)
        {
            var a = new RouletOperator();
            int rend = Random.Range(0, 2);

            if (rend > 0)
            {
                a.Operator1 = plus;
                a.Operator2 = plus;
            }
            else
            {
                a.Operator1 = minus;
                a.Operator2 = minus;
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