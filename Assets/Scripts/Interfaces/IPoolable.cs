namespace CoreBreach.Interfaces
{
    public interface IPoolable
    {
        void OnSpawnFromPool();
        void OnReturnToPool();
    }
}