using System.Data;

namespace TaskAppBackEnd.Model
{
    public class TaskModel
    {
        public int Id { get; set; }
        public string? NameTask { get; set; }
        public string? Description { get; set; }
        public DateTime? Fecha { get; set; }
        public string? UserGestion { get; set; }
        public string? Priority { get; set; } //alta, media, baja
        public string? State { get; set; }  //gestionado, pendiente, en proces o
    }
}
