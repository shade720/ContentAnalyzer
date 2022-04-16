using Common.EntityFramework;
using Microsoft.EntityFrameworkCore;

namespace Common;

public abstract class DatabaseClient<T> : Database
{
    protected DatabaseClient(DbContextOptions<CommentsContext> options) : base(options) { }
    public abstract void Add(T result);
    public abstract GetRangeResult GetRange(int startIndex);
    public abstract void Clear();

    public class GetRangeResult
    {
        public List<T> Result { get; set; }
    }
}