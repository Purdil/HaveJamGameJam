using UnityEngine;


namespace BBJ
{
    public interface IWearOutItem:IDestroyItem, IItem
    {
        public int Durability { get; } // 아이템 내구도
        public void WearOut();
    }
}
