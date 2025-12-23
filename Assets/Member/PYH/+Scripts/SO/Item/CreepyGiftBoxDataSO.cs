using System;
using BBJ;
using Core.Logger;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "CreepyGiftBox", menuName = "SO/ITEM/CreepyGiftBox")]
    public class CreepyGiftBoxDataSO : ItemSO, IBeforeApplyTrunItem<ApplyRouletNum>, IWearOutItem, IProbabilityItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [SerializeField] private OperatorSO Pow;

        public void BeforeApply(Action<ApplyRouletNum> numSetter)
        {
            var a = new ApplyRouletNum(applyOperator :Pow);
            int rend = Random.Range(0, 3);

            switch (rend)
            {
                case 1:
                    {
                        a.RouletNum.Num1 = 2;
                        break;
                    }
                case 2:
                    {
                        a.RouletNum.Num2 = 2;
                        break;
                    }
                case 3:
                    {
                        a.RouletNum.Num3 = 2;
                        break;
                    }
            }

            numSetter(a);
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
