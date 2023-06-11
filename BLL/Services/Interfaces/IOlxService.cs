namespace BLL.Services.Interfaces
{
    public interface IOlxService
    {
        Task<string> ParsePrice(string url);
    }
}