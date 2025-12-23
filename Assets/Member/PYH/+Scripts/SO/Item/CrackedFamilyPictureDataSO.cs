using System;
using BBJ;
using Core.Logger;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "CrackedFamilyPictureDataSO", menuName = "SO/ITEM/CrackedFamilyPictureDataSO")]
    public class CrackedFamilyPictureDataSO : ItemSO, IAfterApplyTrunIteem<RouletOperator, RouletNum>
    {
        [field: SerializeField] public float Probability { get; private set; }
        
        public void AfterApply(Func<RouletOperator> Getter, Action<RouletNum> Setter)
        {
            Logging.Log("Player Health++");
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }

    }
}
