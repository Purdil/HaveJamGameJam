
using System;
using UnityEngine;

namespace Member.YDW.CombatSystem
{
    public abstract class AbstractRandomEvent : MonoBehaviour
    {
        [field: SerializeField] public string Desc { get; private set; }
        
        public abstract void ActiveEvent(Action callback);

    }
}