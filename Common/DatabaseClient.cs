namespace Common;

public abstract class DatabaseClient<T>
{
    public abstract void Add(T result);
    public abstract GetRangeResult GetRange(int startIndex);
    public abstract void Clear();

    public class GetRangeResult
    {
        public GetRangeResult(List<T> result)
        {
            Result = result;
        }

        public List<T> Result { get; }
    }
}