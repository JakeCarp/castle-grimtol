using System.Collections.Generic;
using CastleGrimtol.Project.Interfaces;

namespace CastleGrimtol.Project
{
    public class GameService : IGameService
    {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    private bool playing { get; set; }


    public void GetUserInput()
    {
      //parsing user input
      #region 
      String rawUserInput = Console.ReadLine();
      String[] split = rawUserInput.Split(' ');
      String command = split[0].ToLower();
      String modifier = split[1].ToLower();


      #endregion
      switch (command)
      {
        case "go":
          Go(modifier);
          break;
        case "help":
          Help();
          break;
        case "look":
          Look();
          break;
        case "quit":
          Quit();
          break;
        case "use":
          UseItem("modifier");
          break;
        case "take":
          TakeItem(modifier);
          break;
        case "inventory":
          Inventory();
          break;
      }
    }

    public void Go(string direction)
    {
      if (!CurrentRoom.Exits.ContainsKey(direction))
      {
        System.Console.WriteLine("There's Nothing in that Direction");
        return;
      }
      CurrentRoom = CurrentRoom.Exits[direction];
    }

    }
}