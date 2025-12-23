using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "FILENAME", menuName = "MENUNAME", order = 0)]
    public class DoubleSidedMirrorDataSO : ItemSO, IAfterApplyTrunIteem<RouletNum, RouletNum>, IWearOutItem, IProbabilityItem
    {
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        [field: SerializeField] public float Probability { get; private set; }


        public void AfterApply(Func<RouletNum> getter, Action<RouletNum> setter)
        {
            var a = new RouletNum();
            var read = getter();

            int rend = Random.Range(0, 2);

            if (rend > 0)
            {
                a.Num1 = 0;
                a.Num2 = 0;
                a.Num3 = 0;
            }
            else
            {
                a.Num1 = (int)Mathf.Pow((float)read.Num1, 2);
                a.Num2 = (int)Mathf.Pow((float)read.Num2, 2);
                a.Num3 = (int)Mathf.Pow((float)read.Num3, 2);
            }

            setter(a);
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