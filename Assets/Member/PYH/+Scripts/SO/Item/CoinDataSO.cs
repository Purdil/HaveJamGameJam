using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "CoinDataSO", menuName = "SO/ITEM/CoinDataSO")]
    public class CoinDataSO : ItemSO, IBeforeApplyTrunItem<ApplyRouletNum>, IProbabilityItem
    {
        [field:SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO @operator;
        
        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum();
            a.ApplyNum.Num1 = 5;
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