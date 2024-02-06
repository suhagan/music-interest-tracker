using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using API.Models.DTO;
using API.Models.ViewModels;
using API.Repositories;
using API.Service;
using Microsoft.AspNetCore.Mvc;

namespace API.Handlers
{
  public static class Handler
  {
    // Adds top 10 songs of Sweden from last.fm to the database
    public static async Task<IResult> AddSongsFromLastFm(ApiService apiService, IdbRepository dbRepository)
    {
      try
      {
        var songs = await apiService.GetSongsFromExternalApi();
        var songJson = JsonSerializer.Deserialize<JsonDocument>(songs);

        if (songJson != null)
        {
          var tracks = songJson.RootElement.GetProperty("tracks").GetProperty("track");

          foreach (var track in tracks.EnumerateArray())
          {
            Console.WriteLine("Song name: " + track.GetProperty("name").ToString());
            Console.WriteLine("Song Artist: " + track.GetProperty("artist").GetProperty("name").ToString());

            var genres = await apiService.GetGenreOfASong(track.GetProperty("name").ToString(), track.GetProperty("artist").GetProperty("name").ToString());
            var genresJson = JsonSerializer.Deserialize<JsonDocument>(genres);

            var genre = genresJson.RootElement.GetProperty("track").GetProperty("toptags").GetProperty("tag");

            if (genre.GetArrayLength() > 0)
            {
              Console.WriteLine("Song Genre: " + genre[0].GetProperty("name").ToString());
              await dbRepository.AddSongToDb(new Song { Title = track.GetProperty("name").ToString() }, new Artist { Name = track.GetProperty("artist").GetProperty("name").ToString() }, new Genre { Title = genre[0].GetProperty("name").ToString() });
            }
          }
        }

        return Results.Ok(songJson);
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> AddUser(HttpContext httpContext, IdbRepository dbRepository)
    {
      try
      {
        var body = await httpContext.Request.ReadFromJsonAsync<User>();

        if (body != null)
        {
          var user = await dbRepository.CheckUserExists(body.Username);

          if (user == null)
          {
            await dbRepository.AddUserToDb(body);
            return Results.Ok(body);
          }
          else
          {
            return Results.BadRequest("User already exists");
          }
        }
        else
        {
          return Results.BadRequest("No body");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> GetUsers(IdbRepository dbRepository)
    {
      try
      {
        var users = await dbRepository.GetUsers();

        if (users != null)
        {
          var newUsers = users.Select(u => new UserViewModel
          {
            Id = u.Id,
            Username = u.Username,
          }).ToList();

          return Results.Ok(newUsers);
        }
        else
        {
          return Results.BadRequest("No users");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> ConnectSongToUser(HttpContext httpContext, string username, IdbRepository dbRepository)
    {
      try
      {
        var body = await httpContext.Request.ReadFromJsonAsync<SongDto>();

        if (body != null)
        {
          var song = await dbRepository.CheckSongExists(body.Title);

          if (song != null)
          {

            await dbRepository.ConnectSongToUser(username, song.Title);
            return Results.Ok(body);
          }
          else
          {
            return Results.BadRequest("Song does not exist");
          }
        }
        else
        {
          return Results.BadRequest("No body");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("From handler" + e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> ConnectGenreToUser(HttpContext httpContext, string username, IdbRepository dbRepository)
    {
      try
      {
        var body = await httpContext.Request.ReadFromJsonAsync<GenreDto>();

        if (body != null)
        {
          var genre = await dbRepository.CheckGenreExists(body.Title);

          if (genre != null)
          {
            await dbRepository.ConnectGenreToUser(username, genre.Title);
            return Results.Ok(body);
          }
          else
          {
            return Results.BadRequest("Genre does not exist");
          }
        }
        else
        {
          return Results.BadRequest("No body");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("From handler" + e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }

    }

    public static async Task<IResult> ConnectArtistToUser(HttpContext httpContext, string username, IdbRepository dbRepository)
    {
      try
      {
        var body = await httpContext.Request.ReadFromJsonAsync<ArtistDto>();

        if (body != null)
        {
          var artist = await dbRepository.CheckArtistExists(body.Name);

          if (artist != null)
          {
            await dbRepository.ConnectArtistToUser(username, artist.Name);
            return Results.Ok(body);
          }
          else
          {
            return Results.BadRequest("Artist does not exist");
          }
        }
        else
        {
          return Results.BadRequest("No body");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine("From handler" + e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> GetSongsByUser(string username, IdbRepository dbRepository)
    {
      try
      {
        var songs = await dbRepository.GetSongsOfUser(username);

        if (songs != null)
        {
          var newSongs = songs.Select(s => new SongViewModel
          {
            Id = s.Id,
            Title = s.Title,
          }).ToList();

          return Results.Ok(newSongs);
        }
        else
        {
          return Results.BadRequest("No songs");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> GetGenresByUser(string username, IdbRepository dbRepository)
    {
      try
      {
        var genres = await dbRepository.GetGenresOfUser(username);

        if (genres != null)
        {
          var newGenres = genres.Select(g => new GenreViewModel
          {
            Id = g.Id,
            Title = g.Title,
          }).ToList();

          return Results.Ok(newGenres);
        }
        else
        {
          return Results.BadRequest("No genres");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> GetArtistsByUser(string username, IdbRepository dbRepository)
    {
      try
      {
        var artists = await dbRepository.GetArtistsOfUser(username);

        if (artists != null)
        {
          var newArtists = artists.Select(a => new ArtistViewModel
          {
            Id = a.Id,
            Name = a.Name,
          }).ToList();

          return Results.Ok(newArtists);
        }
        else
        {
          return Results.BadRequest("No artists");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> GetAllSongs(IdbRepository dbRepository)
    {
      try
      {
        var songs = await dbRepository.GetSongs();

        if (songs != null)
        {
          var newSongs = songs.Select(s => new SongViewModel
          {
            Id = s.Id,
            Title = s.Title,
          }).ToList();

          return Results.Ok(newSongs);
        }
        else
        {
          return Results.BadRequest("No songs");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> GetAllGenres(IdbRepository dbRepository)
    {
      try
      {
        var genres = await dbRepository.GetGenres();

        if (genres != null)
        {
          var newGenres = genres.Select(g => new GenreViewModel
          {
            Id = g.Id,
            Title = g.Title,
          }).ToList();

          return Results.Ok(newGenres);
        }
        else
        {
          return Results.BadRequest("No genres");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

    public static async Task<IResult> GetAllArtists(IdbRepository dbRepository)
    {
      try
      {
        var artists = await dbRepository.GetArtists();

        if (artists != null)
        {
          var newArtists = artists.Select(a => new ArtistViewModel
          {
            Id = a.Id,
            Name = a.Name,
          }).ToList();

          return Results.Ok(newArtists);
        }
        else
        {
          return Results.BadRequest("No artists");
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e);
        return Results.StatusCode((int)HttpStatusCode.InternalServerError);
      }
    }

  }
}