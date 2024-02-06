using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using API.Models;
using Client.Utilities;

namespace Client.Functions
{
  public class StartFunctions
  {
    public static async Task Seed()
    {
      try
      {
        var result = ApiHelper.Seed();
        if (result != null)
        {
          Console.WriteLine("Database seeded successfully!");
        }
        else
        {
          Console.WriteLine("Database seeding failed.");
        }

        startingMenuPrompt.startProgram();
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Exception: {ex.Message}");
      }
    }

    public static async Task CreateUser()
    {
      Console.WriteLine("Please enter a username:");
      string username = Console.ReadLine();

      if (username != null)
      {

        try
        {
          var result = await ApiHelper.CreateUser(username);
          if (result != null)
          {
            Console.WriteLine($"User {result.Username} created successfully!");
          }
          else
          {
            Console.WriteLine("User creation failed.");
          }

        }
        catch (Exception ex)
        {
          Console.WriteLine($"Exception: {ex.Message}");
        }
      }

      startingMenuPrompt.startProgram();
    }

    public static async void SelectUser()
    {
      Console.WriteLine("Please enter your username:");
      string username = Console.ReadLine();

      if (username != null)
      {
        var user = await ApiHelper.GetUser(username);
        if (user != null)
        {
          Console.Clear();
          UserFunctions.startUserProgram(user.Username);
        }
        else
        {
          Console.WriteLine("User not found.");
        }

      }
    }

    public static async Task ListUsers()
    {
      var users = await ApiHelper.GetUsers();
      Console.WriteLine("Users:");
      if (users != null)
      {
        foreach (var user in users)
        {
          Console.WriteLine(user.Username);
        }
      }
      else
      {
        Console.WriteLine("No users found.");
      }

      startingMenuPrompt.startProgram();
    }

    public static void ExitProgram()
    {
      Console.Clear();
      Console.WriteLine("Goodbye!");
    }

    public static async void ListSongs()
    {
      var songs = await ApiHelper.GetSongs();
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

      startingMenuPrompt.startProgram();
    }

    public static async void ListArtists()
    {
      var artists = await ApiHelper.GetArtists();
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

      startingMenuPrompt.startProgram();
    }

    public static async void ListGenres()
    {
      var genres = await ApiHelper.GetGenres();
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

      startingMenuPrompt.startProgram();
    }
  }
}