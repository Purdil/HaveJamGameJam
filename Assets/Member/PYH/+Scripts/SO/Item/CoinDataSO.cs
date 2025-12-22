using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "CoinDataSO", menuName = "SO/ITEM/CoinDataSO")]
    public class CoinDataSO : ItemSO, IBeforeApplyTrunItem<ApplyRouletNum>, IProbabilityItem
    {
        [field:SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO plus;
        
        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum();
            a.RouletNum.Num1 = 5;
            a.ApplyOperator = plus;
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