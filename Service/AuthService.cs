using Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTO;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TaskAppBackEnd.Service;

namespace Services
{
    public class AuthService : ResponseService, IAuthService
    {
        private readonly DBManagement _context;
        private readonly string _secretKey;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private static int logID = 0;

        public AuthService(DBManagement context, string secretKey, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _secretKey = secretKey;
            _httpContextAccessor = httpContextAccessor;
        }

        public ReponseModel Authenticate(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                return responseRequired();
            }

            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Email == email);

                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return responseRequired();
                }

                var token = GenerateJwtToken(user);

                return responseSuccess(user);
            }
            catch (Exception ex)
            {
                var info = ErrorService.CatchService2("Authenticate", ex.Message, null, DateTime.Now);
                return responseFailed(info);
            }
        }

        public ReponseModel RegisterUser(string email, string pass, UserModel user)
        {
            int currentLogID = Interlocked.Increment(ref logID);

            try
            {
                var token = Authenticate(email!, pass!);

                if (token.result == null) return responseBadRequest();

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                ErrorService.PrintLogStartRequest(currentLogID.ToString(), "RegisterUser", "RegisterUser", user.Email!, JsonConvert.SerializeObject(user));

                _context.Users.Add(user);
                _context.SaveChanges();

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "RegisterUser", DateTime.Now, user.Email!, "User registered successfully");

                return responseSuccess(user);
            }
            catch (Exception ex)
            {
                var info = ErrorService.CatchService2("RegisterUser", ex.Message, null, DateTime.Now);
                return responseFailed(info);
            }
        }

        private string GenerateJwtToken(UserModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.Email!)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
