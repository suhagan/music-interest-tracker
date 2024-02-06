using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Client.Functions;

namespace Client
{
  public class startingMenuPrompt
  {
        public static void startProgram()
        {
            Console.WriteLine("Please select an option:");
            Console.WriteLine("s. Seed the database");
            Console.WriteLine("1. Create a new user");
            Console.WriteLine("2. Select an existing user");
            Console.WriteLine("3. List all users");
            Console.WriteLine("4. List all songs");
            Console.WriteLine("5. List all artists");
            Console.WriteLine("6. List all genres");
            Console.WriteLine("7. Exit");

            string userInput = Console.ReadLine();
            if (userInput == "s")
            {
                StartFunctions.Seed();
            }
            else if (userInput == "1")
            {
                StartFunctions.CreateUser();
            }
            else if (userInput == "2")
            {
                StartFunctions.SelectUser();
            }
            else if (userInput == "3")
            {
                StartFunctions.ListUsers();
            }
            else if (userInput == "4")
            {
                StartFunctions.ListSongs();
            }
            else if (userInput == "5")
            {
                StartFunctions.ListArtists();
            }
            else if (userInput == "6")
            {
                StartFunctions.ListGenres();
            }
            else if (userInput == "7")
            {
                StartFunctions.ExitProgram();
            }
            else
            {
                Console.WriteLine("Invalid input, please try again.");
            }
        }
    }

}