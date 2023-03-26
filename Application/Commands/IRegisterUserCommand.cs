using Application.DataTransfer;
using Application.Interfaces;

namespace Application.Commands
{
    public interface IRegisterUserCommand : ICommand<UserDTO, string>
    {
    }
}
