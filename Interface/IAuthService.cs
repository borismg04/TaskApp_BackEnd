using Models;
using Models.DTO;

namespace Interfaces
{
    public interface IAuthService
    {
        ResponseModel Authenticate(string email, string password);
        ResponseModel RegisterUser(string email, string pass, UserModel user);

    }
}
