using System;
using BBJ;
using UnityEngine;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "CrackedFamilyPictureDataSO", menuName = "SO/ITEM/CrackedFamilyPictureDataSO")]
    public class CrackedFamilyPictureDataSO : ItemSO, IAfterApplyTrunIteem<RouletOperator, ApplyFinal>
    {
        [field: SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO plus;
        
        public void AfterApply(Func<RouletOperator> Getter, Action<ApplyFinal> Setter)
        {
            var a = new ApplyFinal(32, plus);
            Setter(a);
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }

    }
}
