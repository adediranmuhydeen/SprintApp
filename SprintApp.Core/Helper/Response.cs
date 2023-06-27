namespace SprintApp.Core.Helper
{
    public class Response<T> where T : class
    {
        private readonly string _Message;      
        private readonly T _data;
        private readonly int _statusCode;
        private readonly bool _success;
        public Response(string message, T data, int statusCode, bool success)
        {
            _Message = message;
            _data = data;
            _statusCode = statusCode;
            _success = success;
        }
    }
}
