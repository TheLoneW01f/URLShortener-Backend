namespace URLShortener.EF
{
    public class ShortenURLNanoId
    {
        public string Id { get; set; }
        public string LongURL { get; set; }
        public DateTime CreatedDate { get; set; }
        public long AccessCount { get; set; }

    }
}
