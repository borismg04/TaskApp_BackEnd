using Interfaces;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using TaskAppBackEnd.Service;


namespace Services
{
    public class UsersService : ResponseService, IUsersService
    {
        private readonly DBManagement _context;
        private readonly string _secretKey;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private static int logID = 0;

        public UsersService(DBManagement context, string secretKey, IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            _context = context;
            _secretKey = secretKey;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public ReponseModel GetUsuarios(string? email, string? pass)
        {
            string error = string.Empty;
            DateTime time = DateTime.Now;
            int currentLogID = Interlocked.Increment(ref logID);

            try
            {
                var token = _authService.Authenticate(email!, pass!);

                if (token.result == null)
                {
                    ErrorService.PrintLogStartRequest(currentLogID.ToString(), "GetUsuarios", "GetUsuarios", email!, null!);
                    return responseBadRequest();
                }

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                ErrorService.PrintLogStartRequest(currentLogID.ToString(), "GetUsuarios", "GetUsuarios", email!, null!);

                var users = _context.Users.ToList();

                if (users.Count == 0) return responseNoContent();

                var userDTOs = users.Select(user => new UserResponseDTO
                {
                    Id = user.Id,
                    Nombre = user.Nombre,
                    Email = user.Email,
                    Profile = user.Profile
                }).ToList();

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "GetUsuarios", time, email!, JsonConvert.SerializeObject(userDTOs));

                return responseSuccess(userDTOs);
            }
            catch (Exception ex)
            {
                error = "GetUsuarios: " + ex.Message;
                var info = ErrorService.CatchService2("GetUsuarios", error, null, time);
                return responseFailed(info);
            }
        }

        public ReponseModel UpdateUser(string? email, string? pass, int id, UserModel user)
        {
            string error = string.Empty;
            DateTime time = DateTime.Now;
            int currentLogID = Interlocked.Increment(ref logID);

            try
            {
                var token = _authService.Authenticate(email!, pass!);

                ErrorService.PrintLogStartRequest(logID.ToString(), "UpdateUser", "UpdateUser", email!, pass!);

                if (token.result == null)
                {
                    ErrorService.PrintLogEndRequest(currentLogID.ToString(), "UpdateUser", time, email!, null!);
                    return responseBadRequest();
                }

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                var userToUpdate = _context.Users.FirstOrDefault(x => x.Id == user.Id);

                if (userToUpdate == null)
                {
                    ErrorService.PrintLogEndRequest(currentLogID.ToString(), "UpdateUser", time, email!, null!);
                    return responseNoContent();
                }

                userToUpdate.Nombre = user.Nombre;
                userToUpdate.Email = user.Email;
                userToUpdate.Password = user.Password;
                userToUpdate.Profile = user.Profile;

                _context.Users.Update(userToUpdate);
                _context.SaveChanges();

                var userResponse = new UserResponseDTO
                {
                    Id = userToUpdate.Id,
                    Nombre = userToUpdate.Nombre,
                    Email = userToUpdate.Email,
                    Profile = userToUpdate.Profile
                };

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "UpdateUser", time, email!, JsonConvert.SerializeObject(userResponse));

                return responseSuccess(userResponse);
            }
            catch (Exception ex)
            {
                error = "UpdateUser: " + ex.Message;
                var info = ErrorService.CatchService2("UpdateUser", error, null, time);
                return responseFailed(info);
            }
        }

        public ReponseModel DeleteUser(string? email, string? pass, int id)
        {
            string error = string.Empty;
            DateTime time = DateTime.Now;
            int currentLogID = Interlocked.Increment(ref logID);

            try
            {
                var token = _authService.Authenticate(email!, pass!);

                ErrorService.PrintLogStartRequest(currentLogID.ToString(), "DeleteUser", "DeleteUser", email!, pass!);

                if (token.result == null)
                {
                    ErrorService.PrintLogEndRequest(currentLogID.ToString(), "DeleteUser", time, email!, null!);
                    return responseBadRequest();
                }

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                var UserId = _context.Users.Find(id);

                if (UserId == null)
                {
                    ErrorService.PrintLogEndRequest(currentLogID.ToString(), "DeleteUser", time, email!, null!);
                    return responseNoContent();
                }

                _context.Users.Remove(UserId);
                _context.SaveChanges();

                var userResponse = new UserResponseDTO
                {
                    Id = UserId.Id,
                    Nombre = UserId.Nombre,
                    Email = UserId.Email,
                    Profile = UserId.Profile
                };

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "DeleteUser", time, email!, userResponse.ToString()!);
                return responseSuccess(userResponse);
            }
            catch (Exception ex)
            {
                error = "DeleteUser: " + ex.Message;
                var info = ErrorService.CatchService2("DeleteUser", error, null, time);
                return responseFailed(info);
            }
        }
    }
}
