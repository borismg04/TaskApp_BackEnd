using Models;

namespace TaskAppBackEnd.Service
{
    public class ResponseService
    {
        //200
        public ResponseModel responseSuccess(object value)
        {
            return new ResponseModel
            {
                message = "Operation Success",
                success = true,
                result = value,
                statusCode = 200
            };
        }

        public ResponseModel responseNoContent()
        {
            return new ResponseModel
            {
                message = "Data No Content",
                success = true,
                result = null,
                statusCode = 200
            };
        }
        //500
        public ResponseModel responseFailed(object value)
        {
            return new ResponseModel
            {
                message = "Operation Failed",
                success = true,
                result = value,
                statusCode = 500
            };
        }

        //400
        public ResponseModel responseBadRequest()
        {
            return new ResponseModel
            {
                message = "Undefined",
                success = false,
                result = null,
                statusCode = 401
            };
        }

        public ResponseModel responseRequired()
        {
            return new ResponseModel
            {
                message = "Email and password are required.",
                success = false,
                result = null,
                statusCode = 403
            };

        }

    }
}
