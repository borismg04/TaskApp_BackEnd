using System.ComponentModel.DataAnnotations;

namespace Models.DTO
{
    public class UserModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters")]
        public string? Nombre { get; set; }
        
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string? Email { get; set; }
        
        [Required(ErrorMessage = "Password is required")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string? Password { get; set; }
        
        [RegularExpression("^(SuperAdmin|Admin|User)$", ErrorMessage = "Profile must be 'SuperAdmin', 'Admin', or 'User'")]
        public string? Profile { get; set; }
    }

    public class UserResponseDTO
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? Email { get; set; }
        public string? Profile { get; set; }
    }
}
