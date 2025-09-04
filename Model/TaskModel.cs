using System.ComponentModel.DataAnnotations;

namespace TaskAppBackEnd.Model
{
    public class TaskModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Task name is required")]
        [StringLength(100, ErrorMessage = "Task name cannot exceed 100 characters")]
        public string? NameTask { get; set; }
        
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string? Description { get; set; }
        
        public DateTime? Fecha { get; set; }
        
        [StringLength(100, ErrorMessage = "User management field cannot exceed 100 characters")]
        public string? UserGestion { get; set; }
        
        [RegularExpression("^(alta|media|baja)$", ErrorMessage = "Priority must be 'alta', 'media', or 'baja'")]
        public string? Priority { get; set; } //alta, media, baja
        
        [RegularExpression("^(gestionado|pendiente|en proceso)$", ErrorMessage = "State must be 'gestionado', 'pendiente', or 'en proceso'")]
        public string? State { get; set; }  //gestionado, pendiente, en proceso
    }
}
