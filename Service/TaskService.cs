using Interfaces;
using Models;
using Newtonsoft.Json;
using TaskAppBackEnd.Model;
using TaskAppBackEnd.Service;

namespace Services
{
    public class TaskService : ResponseService, ITaskService
    {
        private readonly DBManagement _context;
        private readonly string _secretKey;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthService _authService;
        private static int logID = 0;

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
            int currentLogID = Interlocked.Increment(ref logID);
            try
            {
                var token = _authService.Authenticate(email!, pass!);

                ErrorService.PrintLogStartRequest(currentLogID.ToString(), "CreateTask", "CreateTask", email!, JsonConvert.SerializeObject(model));

                if (token.result == null) return responseBadRequest();

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                model.Fecha = DateTime.Now;
                _context.Tasks.Add(model);
                _context.SaveChanges();

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "CreateTask", time, email!, JsonConvert.SerializeObject(model));

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
            int currentLogID = Interlocked.Increment(ref logID);

            try
            {
                var token = _authService.Authenticate(email!, pass!);

                ErrorService.PrintLogStartRequest(currentLogID.ToString(), "DeleteTask", "DeleteTask", email!, id!);

                if (token.result == null) return responseBadRequest();

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                var task = _context.Tasks.FirstOrDefault(x => x.Id == id);

                if (task == null)
                {
                    ErrorService.PrintLogStartRequest(currentLogID.ToString(), "DeleteTask", "DeleteTask", email!, id!);
                    return responseNoContent();
                }

                _context.Tasks.Remove(task);
                _context.SaveChanges();

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "DeleteTask", time, email!, JsonConvert.SerializeObject(task));

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
            int currentLogID = Interlocked.Increment(ref logID);

            try
            {
                var token = _authService.Authenticate(email!, pass!);

                ErrorService.PrintLogStartRequest(currentLogID.ToString(), "GetTask", "GetTask", email!, null!);

                if (token.result == null)
                {
                    ErrorService.PrintLogEndRequest(currentLogID.ToString(), "GetTask", time, email!, "Token not found");
                    return responseBadRequest();
                }

                if (_httpContextAccessor.HttpContext != null)
                {
                    _httpContextAccessor.HttpContext.Response.Headers.Append("Authorization", $"Bearer {token}");
                }

                var state = new List<string> { "Gestionado", "Pendiente", "En proceso" };
                var pendingTasks = _context.Tasks
                    .Where(x => state.Contains(x.State!))
                    .ToList();


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

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "GetTask", time, email!, JsonConvert.SerializeObject(result));
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
            int currentLogID = Interlocked.Increment(ref logID);

            try
            {
                var task = _context.Tasks.FirstOrDefault(x => x.Id == model.Id);

                ErrorService.PrintLogStartRequest(currentLogID.ToString(), "UpdateTask", "UpdateTask", email!, JsonConvert.SerializeObject(model));

                if (task == null)
                {
                    ErrorService.PrintLogEndRequest(currentLogID.ToString(), "UpdateTask", time, email!, "Task not found");
                    return responseNoContent();
                }


                task.NameTask = model.NameTask;
                task.Description = model.Description;
                task.Priority = model.Priority;
                task.State = model.State;

                _context.SaveChanges();

                ErrorService.PrintLogEndRequest(currentLogID.ToString(), "UpdateTask", time, email!, JsonConvert.SerializeObject(model));
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
