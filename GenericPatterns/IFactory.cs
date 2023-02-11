namespace medzumi.Utilities.GenericPatterns
{
    public interface IFactory
    {
        public object Create();
    }

    public interface IFactory<out T>
    {
        public T Create();
    }

    public interface IFactory<out TOut, in TIn>
    {
        public TOut Create(TIn tIn);
    }

    public interface IFactory<out TOut, in TIn1, in TIn2>
    {
        public TOut Create(TIn1 tin, TIn2 tin2);
    }
}