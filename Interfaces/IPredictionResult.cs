namespace Common;

public interface IPredictionResult
{
    public ICommentData CommentData { get; }
    public ICategory[] Predicts { get; }
    public string ToString();
}