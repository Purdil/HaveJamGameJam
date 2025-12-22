using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "AceDataSO", menuName = "SO/ITEM/AceDataSO")]
    public class AceDataSo : ItemSO, IBeforeApplyTrunItem<ApplyRouletNum>, IProbabilityItem
    {
        public float Probability { get; private set; }
        [SerializeField] private OperatorSO @operator;
        
        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum();
            a.ApplyNum.Num2 = 8;
            a.ApplyOperator = @operator;
            numSetter(a);
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }
    }
}