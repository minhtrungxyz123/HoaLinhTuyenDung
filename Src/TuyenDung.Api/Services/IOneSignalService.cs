namespace TuyenDung.Api.Services
{
    public interface IOneSignalService
    {
        Task SendAsync(string title, string message, string url);
    }
}
