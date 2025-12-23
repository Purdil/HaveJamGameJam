using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class HeartyMealDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<ApplyRouletNum>, IProbabilityItem
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public float Probability { get; private set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO plus;


        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum(num1: 3, applyOperator: plus );
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