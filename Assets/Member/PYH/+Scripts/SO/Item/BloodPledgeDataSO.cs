using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "BloodPledgeDataSO", menuName = "SO/ITEM/BloodPledgeDataSO")]
    public class BloodPledgeDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>, IProbabilityItem
    {
        [field:SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO plus, minus;
        [SerializeField] private PlayerSelfDamageChannel playerSelfDamageChannel;

        public void BeforeApply(Action<RouletOperator> numSetter)
        {
            playerSelfDamageChannel.Raise(15);
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }

    }
}