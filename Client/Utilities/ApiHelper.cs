using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using API.Models;
using API.Models.DTO;
using API.Models.ViewModels;
using Newtonsoft.Json;

namespace Client.Utilities
{
  public class ApiHelper
  {
    public static string ApiUrl = "https://localhost:7216";

    public static async Task Seed()
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync("/seed").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();

          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
      }
    }

    public static async Task<UserViewModel> CreateUser(string username)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          UserDto user = new UserDto { Username = username };

          string jsonUser = JsonConvert.SerializeObject(user);

          StringContent content = new StringContent(jsonUser, System.Text.Encoding.UTF8, "application/json");

          var response = client.PostAsync("/user", content).Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            UserViewModel newUser = JsonConvert.DeserializeObject<UserViewModel>(responseData);

            return newUser;
          }
          else
          {
            Console.WriteLine(response.ReasonPhrase);
            throw new HttpRequestException(response.ReasonPhrase);
          }
        }
      }
      catch (HttpRequestException ex)
      {
        Console.WriteLine($"HttpRequestException: {ex.Message}");
        throw;
      }
    }

    public static async Task<User> GetUser(string username)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync("/users").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(responseData);

            User selectedUser = users.FirstOrDefault(u => u.Username == username);

            if (selectedUser != null)
            {
              return selectedUser.Username == username ? selectedUser : null;
            }
            else
            {
              throw new Exception("User not found");
            }
          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }

        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<List<User>> GetUsers()
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync("/users").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            List<User> users = JsonConvert.DeserializeObject<List<User>>(responseData);

            return users;
          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<Song> AddSong(string user, string songTitle)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          SongDto song = new SongDto { Title = songTitle };

          string jsonSong = JsonConvert.SerializeObject(song);

          StringContent content = new StringContent(jsonSong, System.Text.Encoding.UTF8, "application/json");
          var response = client.PostAsync($"{user}/song", content).Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            Song newSong = JsonConvert.DeserializeObject<Song>(responseData);

            return newSong;

          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<Artist> AddArtist(string user, string artistName)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          ArtistDto artist = new ArtistDto { Name = artistName };

          string jsonArtist = JsonConvert.SerializeObject(artist);

          StringContent content = new StringContent(jsonArtist, System.Text.Encoding.UTF8, "application/json");
          var response = client.PostAsync($"{user}/artist", content).Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            Artist newArtist = JsonConvert.DeserializeObject<Artist>(responseData);

            return newArtist;

          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<Genre> AddGenre(string user, string genreTitle)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          GenreDto genre = new GenreDto { Title = genreTitle };

          string jsonGenre = JsonConvert.SerializeObject(genre);

          StringContent content = new StringContent(jsonGenre, System.Text.Encoding.UTF8, "application/json");
          var response = client.PostAsync($"{user}/genre", content).Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            Genre newGenre = JsonConvert.DeserializeObject<Genre>(responseData);

            return newGenre;

          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<List<Song>> GetSongs()
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync("/songs").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            List<Song> songs = JsonConvert.DeserializeObject<List<Song>>(responseData);

            return songs;
          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<List<Artist>> GetArtists()
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync("/artists").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            List<Artist> artists = JsonConvert.DeserializeObject<List<Artist>>(responseData);

            return artists;
          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<List<Genre>> GetGenres()
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync("/genres").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            List<Genre> genres = JsonConvert.DeserializeObject<List<Genre>>(responseData);

            return genres;
          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }

    }

    public static async Task<List<Song>> GetSongsByUser(string user)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync($"{user}/songs").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            List<Song> songs = JsonConvert.DeserializeObject<List<Song>>(responseData);

            return songs;
          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<List<Artist>> GetArtistsByUser(string user)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync($"{user}/artists").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            List<Artist> artists = JsonConvert.DeserializeObject<List<Artist>>(responseData);

            return artists;
          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

    public static async Task<List<Genre>> GetGenresByUser(string user)
    {
      try
      {
        using (var client = new HttpClient())
        {
          client.BaseAddress = new Uri(ApiUrl);

          var response = client.GetAsync($"{user}/genres").Result;

          if (response.IsSuccessStatusCode)
          {
            var responseData = await response.Content.ReadAsStringAsync();
            List<Genre> genres = JsonConvert.DeserializeObject<List<Genre>>(responseData);

            return genres;
          }
          else
          {
            throw new Exception(response.ReasonPhrase);
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.Message);
        return null;
      }
    }

  }
}