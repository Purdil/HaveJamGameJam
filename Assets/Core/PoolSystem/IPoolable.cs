namespace Core.PoolSystem
{
    public interface IPoolable
    { 
        public PoolableSO PoolableSO { get; }
        
        public void SettingSO(PoolableSO poolableSO);
        
        public void OnPopObject();
        
        public void OnPushObject();
    }
}