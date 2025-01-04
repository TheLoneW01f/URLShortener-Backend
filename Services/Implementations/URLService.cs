using Microsoft.EntityFrameworkCore;
using URLShortener.EF;
using URLShortener.Services.Contracts;
using URLShortener.Services.Helpers;
using NanoidDotNet;

namespace URLShortener.Services.Implementations
{
    public class URLService : IURLService
    {
        private readonly IConfiguration _config;
        private readonly int NoOfCharsInShortCode;
        private readonly string AlphabetArray;
        private readonly string _baseURL;

        private readonly URLShortenerContext _context;


        public URLService(IConfiguration config, URLShortenerContext context)
        {
            _context = context;
            _config = config;
            NoOfCharsInShortCode = _config.GetValue<int>("ProjectConfig:ShortCodeLength");
            AlphabetArray = _config.GetValue<string>("ProjectConfig:ShortCodeCharacters") ?? "";
            _baseURL = _config.GetValue<string>("ProjectConfig:BaseURL") ?? "";
        }

        public async Task<string> GenerateShortCodeAsync(string OriginalURL)
        {
            if (_context.ShortenedURLNanoIds.Any(x => x.LongURL == OriginalURL))
            {
                return $"{_baseURL}{_context.ShortenedURLNanoIds.FirstOrDefault(x => x.LongURL == OriginalURL).Id}";
            }
            else
            {
                var nanoid = "";
                do
                {
                    nanoid = Nanoid.Generate(AlphabetArray, NoOfCharsInShortCode);
                }
                while (_context.ShortenedURLNanoIds.Any(x => x.Id == nanoid));

                var newEntry = new ShortenURLNanoId
                {
                    Id = nanoid,
                    LongURL = OriginalURL,
                    CreatedDate = DateTime.Now,
                    AccessCount = 0
                };


                _context.ShortenedURLNanoIds.Add(newEntry);

                await _context.SaveChangesAsync();
                return $"{_baseURL}{nanoid}";
            }


        }

        public async Task IncrementAccessCount(string shortCode)
        {
            var updateCount = _context.ShortenedURLNanoIds.FirstOrDefault(x => x.Id == shortCode) ?? throw new KeyNotFoundException("The supplied URL was not found");
            updateCount.AccessCount++;
            await _context.SaveChangesAsync();
        }

        public async Task<string> GetLongURLAsync(string shortCode)
        {
            var getLongURL = await _context.ShortenedURLNanoIds.FirstOrDefaultAsync(x => x.Id == shortCode);
            getLongURL.AccessCount++;
            return getLongURL.LongURL;
        }
    }
}
