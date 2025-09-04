using Models;
using TaskAppBackEnd.Model;

namespace Interfaces
{
    public interface ITaskService
    {
        ResponseModel CreateTask(string? email, string? pass, TaskModel model);
        ResponseModel DeleteTask(string? email, string? pass, int id);
        ResponseModel GetTask(string? email, string? pass);
        ResponseModel UpdateTask(string? email, string? pass, TaskModel model);
        ResponseModel GetTaskAdmin(string? email, string? pass);
    }
}
