using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class CloseHelpDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<RouletOperator>, IProbabilityItem
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public float Probability { get; private set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO plus;
        
        public void BeforeApply(Action<RouletOperator> operatorSetter)
        {
            var a = new RouletOperator(operator1: plus);
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