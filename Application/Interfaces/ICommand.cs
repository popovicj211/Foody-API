namespace Application.Interfaces
{
    public interface ICommand<TRequest>
    {
        void Execute(TRequest request);
    }

    public interface ICommand<TRequest, TResult>
    {
        TResult Execute(TRequest request);
    }
}
