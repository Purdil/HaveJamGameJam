using Core.PoolSystem;
using UnityEngine;

namespace Member.YDW
{
    public class ExplosionEffect : MonoBehaviour, IPoolable
    {
        [field:SerializeField] public PoolableSO PoolableSO { get; private set; }
        private void EndEffect()
        {
            if(gameObject.activeSelf)
                PoolManager.Instance.Factory(PoolableSO).Push(this);
        }

        public void SettingSO(PoolableSO poolableSO)
        {
            
        }

        public void OnPopObject()
        {
        }

        public void OnPushObject()
        {
            
        }
    }
}