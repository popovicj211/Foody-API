namespace Application.Interfaces
{
    public interface IQuery<TSearch, TResult>
    {
        TResult Execute(TSearch search);
    }
}
