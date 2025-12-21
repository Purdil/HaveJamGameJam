namespace Core.PoolSystem
{
    public interface IPoolable
    {
        public int InitialPoolCount { get; }

        public void OnPopObject();
        
        public void OnPushObject();
    }
}