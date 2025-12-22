using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "CreepyGiftBoxDataSO", menuName = "SO/ITEM/CreepyGiftBoxDataSO")]
    public class CreepyGiftBoxDataSO : ItemSO, IBeforeApplyTrunItem<RouletNum>, IWearOutItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }

        public void BeforeApply(Action<RouletNum> numSetter)
        {
            var a = new RouletNum();
            int rend = Random.Range(0, 3);

            switch (rend)
            {
                case 1:
                {
                    a.Num1 = (int)Mathf.Pow((float)a.Num1, 3);
                    break;
                }
                case 2:
                {
                    a.Num2 = (int)Mathf.Pow((float)a.Num1, 3);
                    break;
                }
                case 3:
                {
                    a.Num3 = (int)Mathf.Pow((float)a.Num1, 3);
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
