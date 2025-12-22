using UnityEngine;


namespace BBJ
{
    public interface IWearOut
    {
        public int Durability { get; }
        public void WearOut();
    }
}
