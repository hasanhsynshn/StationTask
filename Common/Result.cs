namespace StationTask.Common
{
    public class Result<T>
    {
        public Result()
        {

        }
        public Result(string message, bool isSuccess, T data)
        {
            Message = message;
            IsSuccess = isSuccess;
            Data = data;
        }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
    }
}
