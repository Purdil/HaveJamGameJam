using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "BloodPledgeDataSO", menuName = "SO/ITEM/BloodPledgeDataSO")]
    public class BloodPledgeDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>, IProbabilityItem, IWearOutItem
    {
        [field:SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO plus;
        [SerializeField] private PlayerSelfDamageChannel playerSelfDamageChannel;
        public Action<IDestroyItem> Destroyed { get; set; }
        [field:SerializeField] public int Durability { get; private set; }
        
        public void BeforeApply(Action<RouletOperator> numSetter)
        {
            playerSelfDamageChannel.Raise(15);
            
            var a = new RouletOperator(operator2: plus);
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