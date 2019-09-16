namespace Nintex.Url.Shortening.Core.ViewModels
{
    public class ServerResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public string Error { get; set; }
    }
}
