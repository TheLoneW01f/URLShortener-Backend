namespace URLShortener.EF
{
    public class ShortenURL
    {
        public Guid Id { get; set; }
        public string LongURL { get; set; } = string.Empty;
        public string ShortURL { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public long AccessCount { get; set; } = 0;

    }
}
