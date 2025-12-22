using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "BloodPledgeDataSO", menuName = "SO/ITEM/BloodPledgeDataSO")]
    public class BloodPledgeDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>, IProbabilityItem
    {
        [field:SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO plus;
        [SerializeField] private PlayerSelfDamageChannel playerSelfDamageChannel;

        public void BeforeApply(Action<RouletOperator> numSetter)
        {
            playerSelfDamageChannel.Raise(15);
            
            var a = new RouletOperator();
            a.Operator2 = plus;
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }

    }
}