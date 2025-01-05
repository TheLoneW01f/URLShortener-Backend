using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using URLShortener.EF;
using Microsoft.Extensions.Configuration;

public class URLShortenerContext(IConfiguration config) : DbContext
{
    public DbSet<ShortenURL> ShortenedURLs { get; set; }
    public DbSet<ShortenURLNanoId> ShortenedURLNanoIds { get; set; }


    public string ConnectionString { get; } = config.GetValue<string>("ProjectConfig:ConnectionStrings") ?? "";

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(ConnectionString);
}
