using Models;
using Models.DTO;

namespace Interfaces
{
    public interface IAuthService
    {
        ReponseModel Authenticate(string email, string password);
        ReponseModel RegisterUser(string email, string pass, UserModel user);

    }
}
