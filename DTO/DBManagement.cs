using Microsoft.EntityFrameworkCore;
using Models.DTO;
using TaskAppBackEnd.Model;

namespace Models
{
    public class DBManagement : DbContext
    {
        public DBManagement(DbContextOptions<DBManagement> options) : base(options)
        {
        }

        public DbSet<UserModel> Users { get; set; }
        public DbSet<TaskModel> Tasks { get; set; }

        public static void Seed(DBManagement context)
        {
            context.Database.EnsureCreated();

            if (context.Users.Any())
                return;

            // Super Admin
            var user = new UserModel
            {
                Nombre = "admin",
                Email = "admin@ad.com",
                Profile = "SurperAdmin",
                Password = DecodeFromBase64("YWRtaW5pc3RyYXRvcg==")

            };

            context.Users.Add(user);
            context.SaveChanges();
        }

        public static string EncodeToBase64(string plainText)
        {
            var plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainBytes);
        }

        public static string DecodeFromBase64(string base64Encoded)
        {
            var base64Bytes = Convert.FromBase64String(base64Encoded);
            return System.Text.Encoding.UTF8.GetString(base64Bytes);
        }
    }
}
