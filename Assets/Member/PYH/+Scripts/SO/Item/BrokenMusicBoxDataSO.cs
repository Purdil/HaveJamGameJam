using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "BrokenMusicBoxDataSO", menuName = "SO/ITEM/BrokenMusicBoxDataSO")]
    public class BrokenMusicBoxDataSO : ItemSO, IBeforeApplyTrunItem<RouletNum>, IProbabilityItem, IWearOutItem
    {
        [field: SerializeField] public float Probability { get; private set; }
        public Action<IDestroyItem> Destroyed { get; set; }
        [field: SerializeField] public int Durability { get; private set; }
        
        public void BeforeApply(Action<RouletNum> numSetter)
        {
            var a = new RouletNum();
            int rend = Random.Range(0, 100);
            
            switch (Random.Range(1, 4))
            {
                case 1:
                {
                    a.Num1 = 0;
                    break;
                }
                case 2:
                {
                    a.Num2 = 0;
                    break;
                }
                case 3:
                {
                    a.Num3 = 0;
                    break;
                }
            }
            
            numSetter(a);
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
