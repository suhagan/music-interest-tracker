using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Service;
using API.Handlers;
using API.Repositories;
using API.Models;

var builder = WebApplication.CreateBuilder(args);
string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(connectionString));
builder.Services.AddScoped<ApiService>();
builder.Services.AddScoped<IdbRepository, DbRepository>();
builder.Services.AddHttpClient();

var app = builder.Build();

// Populate the database with songs from external API
app.MapGet("/seed", async (ApiService apiService, IdbRepository dbRepository) => await Handler.GetSongs(apiService, dbRepository));

// Get and add users
app.MapPost("/user", async (HttpContext httpContext, IdbRepository dbRepository) => await Handler.AddUser(httpContext, dbRepository));
app.MapGet("/users", async (IdbRepository dbRepository) => await Handler.GetUsers(dbRepository));

// Connect user to song, genre, artist
app.MapPost("{username}/song", async (HttpContext httpContext, string username, IdbRepository dbRepository) => await Handler.ConnectSongToUser(httpContext, username, dbRepository));
app.MapPost("{username}/genre", async (HttpContext httpContext, string username, IdbRepository dbRepository) => await Handler.ConnectGenreToUser(httpContext, username, dbRepository));
app.MapPost("{username}/artist", async (HttpContext httpContext, string username, IdbRepository dbRepository) => await Handler.ConnectArtistToUser(httpContext, username, dbRepository));

// Get songs, genres, artists of user
app.MapGet("{username}/songs", async (string username, IdbRepository dbRepository) => await Handler.GetSongsByUser(username, dbRepository));
app.MapGet("{username}/genres", async (string username, IdbRepository dbRepository) => await Handler.GetGenresByUser(username, dbRepository));
app.MapGet("{username}/artists", async (string username, IdbRepository dbRepository) => await Handler.GetArtistsByUser(username, dbRepository));

app.Run();
