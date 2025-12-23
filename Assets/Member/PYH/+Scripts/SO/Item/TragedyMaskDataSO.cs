using System;
using BBJ;
using Core.Logger;
using Member.YDW.EventStruct;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class TragedyMaskDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<RouletOperator>, IProbabilityItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO multiply;
        [SerializeField] private EscapeValueEvent escapeEvent;
        
        public void BeforeApply(Action<RouletOperator> operatorSetter)
        {
            escapeEvent?.Raise(0);
            
            var a = new RouletOperator();
            int rend = Random.Range(0, 2);

            if (rend > 0)
            {
                a.Operator1 = multiply;
            }
            else
            {
                a.Operator2 = multiply;
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