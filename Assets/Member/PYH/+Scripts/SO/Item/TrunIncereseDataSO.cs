using System;
using BBJ;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "TrunIncereseDataSO", menuName = "SO/ITEM/TrunIncereseDataSO")]
    public class TrunIncereseDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletOperator, ApplyRouletNum>
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO multiply;
        
        public void AfterApply(Func<RouletOperator> Getter, Action<ApplyRouletNum> Setter)
        {
            Logging.Log("플레이어 턴 추가");
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