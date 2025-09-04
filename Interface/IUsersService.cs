using Models;
using Models.DTO;

namespace Interfaces
{
    public interface IUsersService
    {
        ResponseModel GetUsuarios(string? user , string? pass);
        ResponseModel UpdateUser(string? email, string? pass, int id, UserModel user);
        ResponseModel DeleteUser(string? email, string? pass, int id);
    }
}
