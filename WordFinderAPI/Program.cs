using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using WordFinderAPI.Core.Models;
using WordFinderAPI.Infrastructure.CacheProvider;
using WordFinderAPI.Interfaces;
using WordFinderAPI.Interfaces.Cache;
using WordFinderAPI.Services;
using WordFinderAPI.Services.Cache;
using WordFinderAPI.Services.Validator;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Business WordFinderAPI.Services.
builder.Services.AddScoped<IWordFinderService, WordFinderService>();

// Cache WordFinderAPI.Services.
builder.Services.AddScoped<IWordFinderCache, WordFinderCache>();

// Fluent Validation.
builder.Services.AddScoped<IValidator<WordFinder>, WordFinderValidator>();

// Redis.
builder.Services.Configure<RedisOptions>(options => builder.Configuration.GetSection(RedisOptions.Redis).Bind(options));

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

// Minimal WordFinder Endpoints.
app.MapPost("/api/word-finder", (PostWordFinder));
Task<IResult> PostWordFinder([FromBody] WordFinder wordFinder, IWordFinderService wordFinderService)
{
    IEnumerable<string> wordsFound = wordFinderService.Find(wordFinder);
    return Task.FromResult(Results.Ok(wordsFound));
};
    
app.Run();