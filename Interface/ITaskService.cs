using Models;
using TaskAppBackEnd.Model;

namespace Interfaces
{
    public interface ITaskService
    {
        ReponseModel CreateTask(string? email, string? pass, TaskModel model);
        ReponseModel DeleteTask(string? email, string? pass, int id);
        ReponseModel GetTask(string? email, string? pass);
        ReponseModel UpdateTask(string? email, string? pass, TaskModel model);
    }
}
