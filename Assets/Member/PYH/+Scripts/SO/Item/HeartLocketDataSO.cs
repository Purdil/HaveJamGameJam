using System;
using BBJ;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "HeartLocketDataSO", menuName = "SO/ITEM/HeartLocketDataSO")]
    public class HeartLocketDataSO : ItemSO, IWearOutItem, IBeforeApplyTrunItem<RouletOperator>, IAfterApplyTrunIteem<RouletNum, ApplyFinal>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO plus;
        
        public void BeforeApply(Action<RouletOperator> operatorSetter)
        {
            Logging.Log("도망치는 확률 감소!!");
        }
        public void AfterApply(Func<RouletNum> Getter, Action<ApplyFinal> Setter)
        {
            var a = new ApplyFinal(30, plus);
            Setter(a);
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