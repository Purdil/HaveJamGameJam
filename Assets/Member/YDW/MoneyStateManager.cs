using System;
using Core;
using Member.PYH._Scripts.Debt;
using UnityEngine;

namespace Member.YDW
{
    public class MoneyStateManager : MonoSingleton<MoneyStateManager>
    {
        public int state {get; private set;}
        protected override void Awake()
        {
            base.Awake();
            DontDestroyOnLoad(this);
            state = DebtManager.Instance.GetDebted();
        }

        private void Update()
        {
            if(state != DebtManager.Instance.GetDebted())
                state = DebtManager.Instance.GetDebted();
        }
    }
}