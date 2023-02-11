using System;

namespace medzumi.Utilities.GenericPatterns.Datas
{
    [Serializable]
    public class Tuple<T1, T2>
    {
        public T1 Item1;
        public T2 Item2;
    }

    [Serializable]
    public class Tuple<T1, T2, T3>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
    }

    [Serializable]
    public class Tuple<T1, T2, T3, T4>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
    }
    
    [Serializable]
    public class Tuple<T1, T2, T3, T4, T5>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
    }

    [Serializable]
    public struct ValueTuple<T1, T2>
    {
        public T1 Item1;
        public T2 Item2;
    }

    [Serializable]
    public struct ValueTuple<T1, T2, T3>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
    }
    
    [Serializable]
    public struct ValueTuple<T1, T2, T3, T4>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
    }
    
    [Serializable]
    public struct ValueTuple<T1, T2, T3, T4, T5>
    {
        public T1 Item1;
        public T2 Item2;
        public T3 Item3;
        public T4 Item4;
        public T5 Item5;
    }
}