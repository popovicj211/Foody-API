namespace Application.Interfaces
{
    public interface ICommandAsync<TRequest>
    {
        Task Execute(TRequest request);
    }
}
