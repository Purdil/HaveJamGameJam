using System;
using BBJ;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Member.PYH._Scripts.SO.Item
{
    [CreateAssetMenu(fileName = "BlackHeadstoneDataSO", menuName = "SO/ITEM/BlackHeadstoneDataSO")]
    public class BlackHeadstoneDataSO : ItemSO, IBeforeApplyTrunItem<RouletOperator>, IProbabilityItem
    {
        [field:SerializeField] public float Probability { get; private set; }
        [SerializeField] private OperatorSO plus, minus;

        public void BeforeApply(Action<RouletOperator> numSetter)
        {
            var a = new RouletOperator();
            int rend = Random.Range(0, 2);

            if (rend > 0)
                a.Operator1 = plus;
            else
                a.Operator1 = minus;

            numSetter(a);
        }

        public void Acquire()
        {
        }
        public void UnAcquire()
        {
        }

    }
}