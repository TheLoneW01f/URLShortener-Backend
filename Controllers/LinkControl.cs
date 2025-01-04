using Microsoft.AspNetCore.Mvc;
using System.Net;
using URLShortener.Services.Contracts;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace URLShortener.Controllers
{
    [ApiController]
    public class LinkControl : ControllerBase
    {
        private readonly IURLService _urlService;

        /*
        * The format for cache dictionary is OriginalURL, ShortenedURL
        * So the OriginalURL becomes the key
        */
        private static Dictionary<string, string> _cache = [];
        private readonly string _baseURL;


        public LinkControl(IURLService UrlService, IConfiguration _config)
        {
            _urlService = UrlService;
            _baseURL = _config.GetValue<string>("ProjectConfig:BaseURL") ?? "";
        }

        [HttpPost]
        [Route("[controller]/Shorten")]
        public async Task<IResult> Shorten(string OriginalURL)
        {

            if (Uri.TryCreate(OriginalURL, UriKind.Absolute, out _))
            {

                if (_cache.ContainsValue(OriginalURL))
                {
                    return Results.Ok(_cache.FirstOrDefault(x => x.Value == OriginalURL).Key);
                }

                var ShortURL = await _urlService.GenerateShortCodeAsync(OriginalURL);
                _cache.Add(ShortURL, OriginalURL);
                return Results.Ok(ShortURL);
            }
            else
            {
                return Results.UnprocessableEntity("Supplied URL is invalid!");
            }

        }

        [HttpGet]
        [Route("/{shortCode}")]
        public async Task<IResult> Redirection(string shortCode)
        {
            var shortURL = $"{_baseURL}{shortCode}";
            if (_cache.ContainsKey(shortURL))
            {
                var LongUrlRetrieve = _cache.GetValueOrDefault(shortURL);
                await _urlService.IncrementAccessCount(shortCode);
                return Results.Redirect(LongUrlRetrieve, true);
            }
            else
            {
                var LongURL = await _urlService.GetLongURLAsync(shortCode);
                _cache.Add(shortURL, LongURL);
                return Results.Redirect(LongURL);
            }
        }
    }
}
