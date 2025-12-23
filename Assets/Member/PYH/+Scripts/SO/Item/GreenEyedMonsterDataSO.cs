using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class GreenEyedMonsterDataSO : ItemSO, IWearOutItem, IAfterApplyTrunIteem<RouletNum, ApplyFinal>, IProbabilityItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO plus;
        
        public void AfterApply(Func<RouletNum> Getter, Action<ApplyFinal> Setter)
        {
            var a = Getter;
            var b = new ApplyFinal();
            b.final = Random.Range(0, 100);
            b.ApplyOperator = plus;
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