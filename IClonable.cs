namespace medzumi.Utilities
{
    public interface IClonable<out T> where T : IClonable<T>
    {
        T Clone();
    }

    public static class ClonableExtensions
    {
        public static TClone CloneResolve<TClone>(this TClone clonable) where TClone : IClonable<TClone>
        {
            return clonable.Clone();
        }
    }
}