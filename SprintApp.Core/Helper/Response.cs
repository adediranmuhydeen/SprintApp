namespace SprintApp.Core.Helper
{
    public class Response<T> where T : class
    {
        public string Message;      
        public T _data;
        public int _statusCode;
        public bool _success;
        public Response(string message, T data, int statusCode, bool success)
        {
           
        }
    }
}
