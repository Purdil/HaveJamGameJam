
using System;

namespace BBJ
{
    public interface IBeforeApplyTrunItem<T> : IItem where T : IRouletApply
    {
        public void BeforeApply(Action<T> numSetter);
    }
}
