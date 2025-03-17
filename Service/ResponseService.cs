using Models;

namespace TaskAppBackEnd.Service
{
    public class ResponseService
    {
        //200
        public ReponseModel responseSuccess(object value)
        {
            return new ReponseModel
            {
                message = "Operation Success",
                success = true,
                result = value,
                statusCode = 200
            };
        }

        public ReponseModel responseNoContent()
        {
            return new ReponseModel
            {
                message = "data No Content",
                success = true,
                result = null,
                statusCode = 200
            };
        }
        //500
        public ReponseModel responseFailed(object value)
        {
            return new ReponseModel
            {
                message = "Operation Failed",
                success = true,
                result = value,
                statusCode = 500
            };
        }

        //400
        public ReponseModel responseBadRequest()
        {
            return new ReponseModel
            {
                message = "Undefined",
                success = false,
                result = null,
                statusCode = 401
            };
        }

        public ReponseModel responseRequired()
        {
            return new ReponseModel
            {
                message = "Email and password are required.",
                success = false,
                result = null,
                statusCode = 400
            };

        }

    }
}
