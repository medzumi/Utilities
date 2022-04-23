namespace ApplicationScripts.Ecs.Utility
{
    public interface IClonable<T> where T : IClonable<T>
    {
        T Clone();
    }
}