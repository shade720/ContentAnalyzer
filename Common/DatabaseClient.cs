using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Common;

public abstract class DatabaseClient<T> : Database
{
    //protected DatabaseClient() { }
    public abstract void Add(T result);
    public abstract GetRangeResult GetRange(int startIndex);
    public abstract void Clear();

    public class GetRangeResult
    {
        public List<T> Result { get; set; }
    }
}