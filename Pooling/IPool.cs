namespace Utilities.Pooling
{
    public interface IPool<T>
    {
        T Get();
        void Release(T tObject);
    }
}