using System.Collections.Generic;
using System;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Playing { get; set; }


      #endregion
      //creating items
      #region 
      Item wornHammer = new Item("Worn Hammer", "A very old hammer that your father gave you. It's seen more than it's fair share of work, but it hasn't failed you yet.");

      #endregion
      //adding Exits
      #region 
      constructionSite.Exits.Add("east", wreckedClearing);
      wreckedClearing.Exits.Add("west", constructionSite);
      wreckedClearing.Exits.Add("south", smallBunkerEntrance);
      smallBunkerEntrance.Exits.Add("north", wreckedClearing);
      smallBunkerEntrance.Exits.Add("down", hallway1);
      hallway1.Exits.Add("up", smallBunkerEntrance);
      hallway1.Exits.Add("south", storage1);
      hallway1.Exits.Add("southwast", securityCheck1);
      hallway1.Exits.Add("east", hallway2);
      storage1.Exits.Add("north", hallway1);
      securityCheck1.Exits.Add("northwest", hallway1);
      securityCheck1.Exits.Add("southeast", hallway2);
      hallway2.Exits.Add("west", hallway1);
      hallway2.Exits.Add("southwest", securityCheck1);

      #endregion
      //adding items
      #region 
      constructionSite.Items.Add(wornHammer);
      #endregion
      //build item use dictionary
      #region 
      itemUseGuide = new Dictionary<string, string>();
      itemUseGuide.Add("worn hammer-lock", "thing happens");
      itemUseGuide.Add("worn hammer-jack", "thing happens");
      #endregion
      CurrentRoom = constructionSite;
    }
    public gameActions Parse(string text)
    {
      string input = text.ToLower();
      if (input.Contains("look"))
      {
        return gameActions.lookRoom;
      };
      //movement
      #region 
      if (input.Contains("go") && input.Contains("north"))
      {
        return gameActions.goNorth;
      };
      if (input.Contains("go") && input.Contains("east"))
      {
        return gameActions.goEast;
      };
      if (input.Contains("go") && input.Contains("south"))
      {
        return gameActions.goSouth;
      };
      if (input.Contains("go") && input.Contains("west"))
      {
        return gameActions.goWest;
      };
      if (input.Contains("go") && input.Contains("down"))
      {
        return gameActions.goDown;
      };
      if (input.Contains("go") && input.Contains("up"))
      {
        return gameActions.goUp;
      };
      if (input.Contains("go") && input.Contains("northeast"))
      {
        return gameActions.goNorthEast;
      };
      if (input.Contains("go") && input.Contains("northwest"))
      {
        return gameActions.goNorthWest;
      };
      if (input.Contains("go") && input.Contains("southeast"))
      {
        return gameActions.goSouthEast;
      };
      if (input.Contains("go") && input.Contains("southwest"))
      {
        return gameActions.goSouthWest;
      };
      if (input.Contains("go") && input.Contains("stairs") && input.Contains("up"))
      {
        return gameActions.goUp;
      }
      if (input.Contains("go") && input.Contains("stairs") && input.Contains("down"))
      {
        return gameActions.goDown;
      }
      #endregion
      //console commands
      #region
      if (input.Contains("help"))
      {
        return gameActions.help;
      };
      if (input.Contains("inventory"))
      {
        return gameActions.playerInventory;
      };
      if (input.Contains("quit"))
      {
        return gameActions.gameQuit;
      };
      if (input.Contains("reset"))
      {
        return gameActions.gameReset;
      };
      #endregion
      if (input.Contains("use") && input.Contains("hammer") && input.Contains("lock"))
      {
        return gameActions.useHammerOnLock;
      };
      if (input.Contains("take") && input.Contains("hammer"))
      {
        return gameActions.takeHammer;
      }

      return gameActions.wait;
    }
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

    public void Help()
    {
      System.Console.WriteLine("this is help");
    }

    public void Inventory()
    {
      System.Console.WriteLine("You currently have:");
      CurrentPlayer.Inventory.ForEach(i =>
      {
        System.Console.WriteLine($">{i.Name}");
      });
    }

    public void Look()
    {
      System.Console.WriteLine(CurrentRoom.Description);
    }

    public void Quit()
    {
      Playing = false;
    }

    public void Reset()
    {

    }

    public void Setup()
    {
      Playing = true;
      //creating rooms
      #region 
      Room constructionSite = new Room("Construction Site", "You are In construction site");
      Room wreckedClearing = new Room("Wrecked Clearing", "You are In wrecked clearing");
      Room smallBunkerEntrance = new Room("Small Bunker Entrance", "You are In small bunker entrance");
      Room hallway1 = new Room("Hallway 1", "You are In hallway 1");
      Room storage1 = new Room("Storage 1", "You are In storage 1");
      Room securityCheck1 = new Room("Security Checkpoint 1", "You are In security checkpoint 1");
      Room hallway2 = new Room("Hallway 2", "You are In hallway 2");

      #endregion
      //creating items
      #region 
      Item wornHammer = new Item("Worn Hammer", "A very old hammer that your father gave you. It's seen more than it's fair share of work, but it hasn't failed you yet.");

      #endregion
      //adding Exits
      #region 
      constructionSite.Exits.Add("east", wreckedClearing);
      wreckedClearing.Exits.Add("west", constructionSite);
      wreckedClearing.Exits.Add("south", smallBunkerEntrance);
      smallBunkerEntrance.Exits.Add("north", wreckedClearing);
      smallBunkerEntrance.Exits.Add("down", hallway1);
      #endregion
      //adding items
      #region 
      constructionSite.Items.Add(wornHammer);
      #endregion
      CurrentRoom = constructionSite;
    }

    public void setCurrentPlayer(string name)
    {
      Player yaBoi = new Player(name);
      CurrentPlayer = yaBoi;
    }
    public void StartGame()
    {
    }
    public void Intro()
    {
      System.Console.WriteLine("starting game");
    }
    public void TakeItem(string itemName)
    {
      Item targetItem = CurrentRoom.Items.Find(i =>
      {
        return i.Name == itemName;
      });
      CurrentPlayer.Inventory.Add(targetItem);
      CurrentRoom.Items.Remove(targetItem);
      System.Console.WriteLine($"You Pickup the {targetItem.Name}");
    }

    public void UseItem(string itemName)
    {

    }
  }
}