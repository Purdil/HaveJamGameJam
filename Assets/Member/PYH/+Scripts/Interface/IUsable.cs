using UnityEngine;

namespace Member.PYH._Scripts.Interface
{
    public interface IUsable
    {
        public LayerMask Target { get; }
        public void UseItem();
    }
}