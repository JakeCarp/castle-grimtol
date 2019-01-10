using System;
using CastleGrimtol.Project;

namespace CastleGrimtol
{
  public class Program
  {
    public static void Main(string[] args)
    {
      Console.Title = "WELCOME TO MY GAME!";

      Console.Clear();
      GameService gameService = new GameService();
      bool appRunning = true;
      while (appRunning)
      {
        System.Console.WriteLine("What is your name?");
        string playerName = Console.ReadLine();
        gameService.setCurrentPlayer(playerName);
        gameService.Setup();
        Console.Clear();
        gameService.Intro();
        var input = Console.ReadKey().Key;
        if (input == ConsoleKey.Enter)
        {
          Console.Clear();
          gameService.Look();
        }
        while (gameService.Playing)
        {
          gameService.GetUserInput();
        }
      }

    }
  }
}
