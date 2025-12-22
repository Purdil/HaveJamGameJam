using System;
using UnityEngine;

namespace BBJ
{
    public interface IAfterApplyTrunIteem<T,V> : IItem where T: IRouletInfo where V: IRouletApply
    {
        public void AfterApply(Func<T> Getter, Action<V> Setter);
    }
}
