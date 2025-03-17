using Models;
using Models.DTO;
using TaskAppBackEnd.Interface;

namespace TaskAppBackEnd.Service
{
    public class LoginService: ResponseService, ILogin
    {
        private readonly DBManagement _context;

        public LoginService(DBManagement context)
        {
            _context = context;

        }

        public ReponseModel Authenticate(string email, string Password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == Password);

            if (user == null) return responseBadRequest();

            var userDTO = new UserResponseDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Profile = user.Profile
            }; 


            return responseSuccess(userDTO);
        }
    }
}
