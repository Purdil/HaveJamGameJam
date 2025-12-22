using System;
using BBJ;
using Core.Logger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "GreenEyedMonsterDataSO", menuName = "SO/ITEM/GreenEyedMonsterDataSO")]
    public class GreenEyedMonsterDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletNum, ApplyFinal>, IBeforeApplyTrunItem<RouletNum>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO plus;
        
        public void BeforeApply(Action<RouletNum> numSetter)
        {
            Logging.Log("도망 확률은 랜덤으로");
        }
        public void AfterApply(Func<RouletNum> Getter, Action<ApplyFinal> Setter)
        {
            var a = Getter;
            var b = new ApplyFinal(final: Random.Range(0, 100), applyOperator: plus);
            Setter(b);
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