using Models;

namespace TaskAppBackEnd.Interface
{
    public interface ILogin
    {
        ResponseModel Authenticate(string name, string pass);
    }
}
