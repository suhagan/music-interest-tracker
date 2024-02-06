using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using API.Models;
using Client.Utilities;

namespace Client.Functions
{
  public class UserFunctions
  {
    private static string user;
    public static void startUserProgram(string selectedUser)
    {
      user = selectedUser;

      Console.WriteLine($"Welcome {user}!");
      UserMenuPrompt();

      static void UserMenuPrompt()
      {
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Connect a song");
        Console.WriteLine("2. Connect an artist");
        Console.WriteLine("3. Connect a genre");
        Console.WriteLine("4. View your songs");
        Console.WriteLine("5. View your artists");
        Console.WriteLine("6. View your genres");
        Console.WriteLine("7. Exit");

        string userInput = Console.ReadLine();
        if (userInput == "1")
        {
          AddSong();
        }
        else if (userInput == "2")
        {
          AddArtist();
        }
        else if (userInput == "3")
        {
          AddGenre();
        }
        else if (userInput == "4")
        {
          ViewSongs();
        }
        else if (userInput == "5")
        {
          ViewArtists();
        }
        else if (userInput == "6")
        {
          ViewGenres();
        }
        else if (userInput == "7")
        {
          ExitProgram();
        }
        else
        {
          Console.WriteLine("Invalid input, please try again.");
        }
      }

      static async void AddSong()
      {
        Console.WriteLine("Please enter the song name:");
        string songTitle = Console.ReadLine();

        if (songTitle != null && user != null)
        {
          var result = await ApiHelper.AddSong(user, songTitle);
          if (result != null)
          {
            Console.WriteLine($"Song {result.Title} added to user {user} successfully!");
          }
          else
          {
            Console.WriteLine("Song addition failed.");
          }

          UserMenuPrompt();
        }
      }

      static async void AddArtist()
      {
        Console.WriteLine("Please enter the artist name:");
        string artistName = Console.ReadLine();

        if (artistName != null && user != null)
        {
          var result = await ApiHelper.AddArtist(user, artistName);
          if (result != null)
          {
            Console.WriteLine($"Artist {result.Name} added to user {user} successfully!");
          }
          else
          {
            Console.WriteLine("Artist addition failed.");
          }

          UserMenuPrompt();
        }
      }

      static async void AddGenre()
      {
        Console.WriteLine("Please enter the genre name:");
        string genreName = Console.ReadLine();

        if (genreName != null && user != null)
        {
          var result = await ApiHelper.AddGenre(user, genreName);
          if (result != null)
          {
            Console.WriteLine($"Genre {result.Title} added to user {user} successfully!");
          }
          else
          {
            Console.WriteLine("Genre addition failed.");
          }

          UserMenuPrompt();
        }
      }

      static async void ViewSongs()
      {
        var songs = await ApiHelper.GetSongsByUser(user);
        if (songs != null)
        {
          foreach (var song in songs)
          {
            Console.WriteLine(song.Title);
          }
        }
        else
        {
          Console.WriteLine("No songs found.");
        }

        UserMenuPrompt();
      }

      static async void ViewArtists()
      {
        var artists = await ApiHelper.GetArtistsByUser(user);
        if (artists != null)
        {
          foreach (var artist in artists)
          {
            Console.WriteLine(artist.Name);
          }
        }
        else
        {
          Console.WriteLine("No artists found.");
        }

        UserMenuPrompt();
      }

      static async void ViewGenres()
      {
        var genres = await ApiHelper.GetGenresByUser(user);
        if (genres != null)
        {
          foreach (var genre in genres)
          {
            Console.WriteLine(genre.Title);
          }
        }
        else
        {
          Console.WriteLine("No genres found.");
        }

        UserMenuPrompt();
      }

      static void ExitProgram()
      {
        Console.Clear();
        startingMenuPrompt.startProgram();
      }
    }
  }
}