namespace URLShortener.Services.Contracts
{
    public interface IURLService
    {
        Task<string> GenerateShortCodeAsync(string OriginalURL);
        Task<string> GetLongURLAsync(string shortCode);
        Task IncrementAccessCount(string shortCode);
        Task<long> GetAccessCount(string shortCode);
    }
}
