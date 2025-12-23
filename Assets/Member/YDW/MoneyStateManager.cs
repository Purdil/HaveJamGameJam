using Core;
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
        }
    }
}