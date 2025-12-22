using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "AnnuityCertificateDataSO", menuName = "SO/ITEM/AnnuityCertificateDataSO")]
    public class AnnuityCertificateDataSO : ItemSO, IBeforeApplyTrunItem<ApplyRouletNum>, IProbabilityItem
    {
        [field:SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO @operator;
        
        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum();
            a.RouletNum.Num1 = 1;
            a.RouletNum.Num2 = 1;
            a.RouletNum.Num3 = 1;
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