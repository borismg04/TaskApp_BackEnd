using Models;

namespace TaskAppBackEnd.Interface
{
    public interface ILogin
    {
        ReponseModel Authenticate(string name, string pass);
    }
}
