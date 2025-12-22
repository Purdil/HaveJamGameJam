using UnityEngine;

namespace BBJ
{
    public interface IAfterApply:IItem
    {
        public void AfterApply(/*현재 데이터Func() Getter, Setter(데이터)*/);
    }
}
