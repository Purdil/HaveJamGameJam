using System;
using BBJ;
using Core.Logger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class TragedyMaskDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<RouletOperator>
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO multiply;
        
        public void BeforeApply(Action<RouletOperator> operatorSetter)
        {
            Logging.Log("도망가는 확률 증가");
            
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