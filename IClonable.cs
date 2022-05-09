namespace Utilities
{
    public interface IClonable<out T> where T : IClonable<T>
    {
        T Clone();
    }
}