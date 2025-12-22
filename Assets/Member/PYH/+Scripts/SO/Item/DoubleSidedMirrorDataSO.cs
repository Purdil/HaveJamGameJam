using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "DoubleSidedMirrorDataSO", menuName = "SO/ITEM/DoubleSidedMirrorDataSO")]
    public class DoubleSidedMirrorDataSO : ItemSO, IBeforeApplyTrunItem<RouletNum>, IWearOutItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        
        public void BeforeApply(Action<RouletNum> numSetter)
        {
            var a = new RouletNum();
            int rend = Random.Range(0, 2);

            if (rend > 0)
            {
                a.Num1 = 0;
                a.Num2 = 0;
                a.Num3 = 0;
            }
            else
            {
                a.Num1 = (int)Mathf.Pow((float)a.Num1, 2);
                a.Num2 = (int)Mathf.Pow((float)a.Num2, 2);
                a.Num3 = (int)Mathf.Pow((float)a.Num3, 2);
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