using Interfaces;
using Microsoft.AspNetCore.Http;
using Models;
using TaskAppBackEnd.Model;
using TaskAppBackEnd.Service;

namespace Services
{
    public class TaskService : ResponseService , ITaskService
    {
        private readonly DBManagement _context;
        private readonly string _secretKey;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;

        public TaskService(DBManagement context, string secretKey, IHttpContextAccessor httpContextAccessor, IAuthService authService)
        {
            _context = context;
            _secretKey = secretKey;
            _httpContextAccessor = httpContextAccessor;
            _authService = authService;
        }

        public ReponseModel CreateTask(string? email, string? pass, TaskModel model)
        {
            string error = string.Empty;
            DateTime time = DateTime.Now;
            try
            {
                var token = _authService.Authenticate(email!, pass!);

                if (token.result == null) return responseBadRequest();

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                model.Fecha = DateTime.Now;
                _context.Tasks.Add(model);
                _context.SaveChanges();

                return responseSuccess(model);
            }
            catch (Exception ex)
            {
                error = "CreateTask: " + ex.Message;
                var info = ErrorService.CatchService2("CreateTask", error, null, time);
                return responseFailed(info);
            }
        }

        public ReponseModel DeleteTask(string? email, string? pass, int id)
        {
            string error = string.Empty;
            DateTime time = DateTime.Now;
            try
            {
                var token = _authService.Authenticate(email!, pass!);

                if (token.result == null) return responseBadRequest();

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

                if (task == null) return responseNoContent();

                _context.Tasks.Remove(task);
                _context.SaveChanges();
                return responseSuccess(task);
            }
            catch (Exception ex)
            {
                error = "DeleteTask: " + ex.Message;
                var info = ErrorService.CatchService2("DeleteTask", error, null, time);
                return responseFailed(info);
            }
        }

        public ReponseModel GetTask(string? email, string? pass)
        {
            string error = string.Empty;
            DateTime time = DateTime.Now;
            try
            {
                var token = _authService.Authenticate(email!, pass!);

                if (token.result == null) return responseBadRequest();

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                var pendingTasks = _context.Tasks.
                    Where(x => x.State == "Pendiente").ToList();

                var priorityCounts = new
                {
                    Alta = pendingTasks.Count(x => x.Priority == "Alta"),
                    Media = pendingTasks.Count(x => x.Priority == "Media"),
                    Baja = pendingTasks.Count(x => x.Priority == "Baja")
                };

                if (pendingTasks.Count == 0) return responseNoContent();

                var orderedTasks = pendingTasks.OrderBy(t =>
                    t.Priority == "Alta" ? 0 :
                    t.Priority == "Media" ? 1 : 2).ToList();

                var result = new
                {
                    tasks = orderedTasks,
                    count = priorityCounts
                };

                return responseSuccess(result);
            }
            catch (Exception ex)
            {
                error = "GetTask: " + ex.Message;
                var info = ErrorService.CatchService2("GetTask", error, null, time);
                return responseFailed(info);
            }
        }

        public ReponseModel UpdateTask(string? email, string? pass, TaskModel model)
        {
            string error = string.Empty;
            DateTime time = DateTime.Now;
            try
            {
                var task = _context.Tasks.FirstOrDefault(x => x.Id == model.Id);

                if (task == null) return responseNoContent();

                task.NameTask = model.NameTask;
                task.Description = model.Description;
                task.Priority = model.Priority;
                task.State = model.State;

                _context.SaveChanges();

                return responseSuccess(task);
            }
            catch (Exception ex)
            {
                error = "UpdateTask: " + ex.Message;
                var info = ErrorService.CatchService2("UpdateTask", error, null, time);
                return responseFailed(info);
            }
        }
    }
}
