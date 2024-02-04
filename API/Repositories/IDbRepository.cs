using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;

namespace API.Repositories
{
  public interface IdbRepository
  {
    Task<List<User>> GetUsers();

    // Add to db methods
    Task AddSongToDb(Song song, Artist artist, Genre genre);
    Task AddArtistToDb(Artist artist);
    Task AddGenreToDb(Genre genre);
    Task AddUserToDb(User user);

    Task<User> CheckUserExists(string username);
    Task<Song> CheckSongExists(string title);
    Task<Genre> CheckGenreExists(string title);
    Task<Artist> CheckArtistExists(string name);

    Task ConnectSongToUser(string username, string songTitle);
    Task ConnectGenreToUser(string username, string genreTitle);
    Task ConnectArtistToUser(string username, string artistName);

    Task<ICollection<Song>> GetSongsOfUser(string username);
    Task<ICollection<Genre>> GetGenresOfUser(string username);
    Task<ICollection<Artist>> GetArtistsOfUser(string username);
  }

  public class DbRepository : IdbRepository
  {
    private readonly ApplicationContext _context;

    public DbRepository(ApplicationContext context)
    {
      _context = context;
    }

    public async Task AddSongToDb(Song song, Artist artist, Genre genre)
    {
      var artistExists = _context.Artist.FirstOrDefault(a => a.Name == artist.Name);
      var genreExists = _context.Genre.FirstOrDefault(g => g.Title == genre.Title);

      if (artistExists == null)
      {
        await AddArtistToDb(artist);
      }

      if (genreExists == null)
      {
        await AddGenreToDb(genre);
      }

      var newSong = new Song
      {
        Title = song.Title,
        Artist = _context.Artist.FirstOrDefault(a => a.Name == artist.Name),
        Genre = _context.Genre.FirstOrDefault(g => g.Title == genre.Title)
      };

      var songExists = _context.Song.FirstOrDefault(s => s.Title == song.Title);

      if (songExists == null)
      {
        _context.Song.Add(newSong);
        _context.SaveChanges();
      }
    }

    public Task AddArtistToDb(Artist artist)
    {
      _context.Artist.Add(artist);
      _context.SaveChanges();

      return Task.CompletedTask;
    }

    public Task AddGenreToDb(Genre genre)
    {
      _context.Genre.Add(genre);
      _context.SaveChanges();

      return Task.CompletedTask;
    }

    public Task AddUserToDb(User user)
    {
      _context.User.Add(user);
      _context.SaveChanges();

      return Task.CompletedTask;
    }

    public Task<User> CheckUserExists(string username)
    {
      var user = _context.User.FirstOrDefault(u => u.Username == username);

      if (user != null)
      {
        return Task.FromResult(user);
      }
      else
      {
        return Task.FromResult<User>(null);
      }
    }

    public Task<Song> CheckSongExists(string title)
    {
      var song = _context.Song.FirstOrDefault(s => s.Title == title);

      if (song != null)
      {
        return Task.FromResult(song);
      }
      else
      {
        return Task.FromResult<Song>(null);
      }
    }

    public Task<Genre> CheckGenreExists(string title)
    {
      var genre = _context.Genre.FirstOrDefault(g => g.Title == title);

      if (genre != null)
      {
        return Task.FromResult(genre);
      }
      else
      {
        return Task.FromResult<Genre>(null);
      }
    }

    public Task<Artist> CheckArtistExists(string name)
    {
      var artist = _context.Artist.FirstOrDefault(a => a.Name == name);

      if (artist != null)
      {
        return Task.FromResult(artist);
      }
      else
      {
        return Task.FromResult<Artist>(null);
      }
    }

    public async Task<List<User>> GetUsers()
    {
      var users = await _context.User.ToListAsync();

      return users;
    }

    public async Task ConnectSongToUser(string username, string title)
    {
      var user = _context.User.Include(u => u.Songs).FirstOrDefault(u => u.Username == username);
      var song = _context.Song.FirstOrDefault(s => s.Title == title);

      if (user != null && song != null)
      {
        user.Songs.Add(song);
        _context.SaveChanges();
      }
    }

    public async Task ConnectGenreToUser(string username, string title)
    {
      var user = _context.User.Include(u => u.Genres).FirstOrDefault(u => u.Username == username);
      var genre = _context.Genre.FirstOrDefault(g => g.Title == title);

      if (user != null && genre != null)
      {
        user.Genres.Add(genre);
        _context.SaveChanges();
      }
    }

    public async Task ConnectArtistToUser(string username, string name)
    {
      var user = _context.User.Include(u => u.Artists).FirstOrDefault(u => u.Username == username);
      var artist = _context.Artist.FirstOrDefault(a => a.Name == name);

      if (user != null && artist != null)
      {
        user.Artists.Add(artist);
        _context.SaveChanges();
      }
    }

    public async Task<ICollection<Song>> GetSongsOfUser(string username)
    {
      var user = _context.User.Include(u => u.Songs).FirstOrDefault(u => u.Username == username);

      if (user != null)
      {
        return user.Songs;
      }
      else
      {
        return new List<Song>();
      }
    }

    public async Task<ICollection<Genre>> GetGenresOfUser(string username)
    {
      var user = _context.User.Include(u => u.Genres).FirstOrDefault(u => u.Username == username);

      if (user != null)
      {
        return user.Genres;
      }
      else
      {
        return new List<Genre>();
      }
    }

    public async Task<ICollection<Artist>> GetArtistsOfUser(string username)
    {
      var user = _context.User.Include(u => u.Artists).FirstOrDefault(u => u.Username == username);

      if (user != null)
      {
        return user.Artists;
      }
      else
      {
        return new List<Artist>();
      }

    }
  }
}