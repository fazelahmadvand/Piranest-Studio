namespace Piranest
{
    [System.Serializable]
    public class Map<T1, T2>
    {
        public T1 Key;
        public T2 Value;

        public Map(T1 key, T2 value)
        {
            Key = key;
            Value = value;
        }
    }
}
