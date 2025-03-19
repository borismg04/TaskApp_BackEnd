using Models;
using Models.DTO;
using Newtonsoft.Json;
using Services;
using TaskAppBackEnd.Interface;

namespace TaskAppBackEnd.Service
{
    public class LoginService : ResponseService, ILogin
    {
        private readonly DBManagement _context;
        private static int logID = 0;

        public LoginService(DBManagement context)
        {
            _context = context;
        }

        public ReponseModel Authenticate(string email, string Password)
        {
            string error = string.Empty;
            DateTime time = DateTime.Now;
            int currentLogID = Interlocked.Increment(ref logID);

            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == email && x.Password == Password);


                ErrorService.PrintLogStartRequest(currentLogID.ToString(), "Authenticate", "Authenticate", email, null!);

                if (user == null) return responseBadRequest();

                //Password = BCrypt.Net.BCrypt.HashPassword(Password);
                //user!.Password = Password;
                //_context.SaveChanges();

                var userDTO = new UserResponseDTO
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    Profile = user.Profile
                };

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "Authenticate", time, email, JsonConvert.SerializeObject(userDTO));

                return responseSuccess(userDTO);

            }
            catch (Exception ex)
            {
                error = "Authenticate: " + ex.Message;
                var info = ErrorService.CatchService2("Authenticate", error, null, time);
                return responseFailed(info);
            }

        }
    }
}
