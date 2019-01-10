using System.Collections.Generic;
using System;
using CastleGrimtol.Project.Interfaces;
using CastleGrimtol.Project.Models;

namespace CastleGrimtol.Project
{
  public class GameService : IGameService
  {
    //building props
    public IRoom CurrentRoom { get; set; }
    public Player CurrentPlayer { get; set; }
    public bool Playing { get; set; }
    public Dictionary<String, String> ItemUseGuide { get; set; }

    public Dictionary<String, String> HazardGuide { get; set; }

    public enum gameActions
    {
      //navigation
      #region
      goSouth,
      goNorth,
      goEast,
      goWest,
      goNorthEast,
      goNorthWest,
      goSouthEast,
      goSouthWest,
      goDown,
      goUp,
      #endregion
      //console commands
      #region
      help,
      wait,
      gameQuit,
      gameReset,
      #endregion
      //context commands
      #region
      lookRoom,
      playerInventory,
      useHammerOnLock,
      useHammerOnMonster,
      useHammerOnPerson,
      takeHammer,
      #endregion
    }
    public void setCurrentPlayer(string name)
    {
      Player yaBoi = new Player(name);
      CurrentPlayer = yaBoi;
    }
    public void Setup()
    {
      Playing = true;
      //creating rooms
      #region 
      Room constructionSite = new Room("Construction Site", "You are In construction site", false);
      Room wreckedClearing = new Room("Wrecked Clearing", "You are In wrecked clearing", false);
      Room smallBunkerEntrance = new Room("Small Bunker Entrance", "You are In small bunker entrance", false);
      Room hallway1 = new Room("Hallway 1", "You are In hallway 1", true);
      Room storage1 = new Room("Storage 1", "You are In storage 1", false);
      Room securityCheck1 = new Room("Security Checkpoint 1", "You are In security checkpoint 1", false);
      Room hallway2 = new Room("Hallway 2", "You are In hallway 2", false);

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
      ItemUseGuide = new Dictionary<string, string>();
      ItemUseGuide.Add("worn hammer-lock", "thing happens");
      ItemUseGuide.Add("worn hammer-jack", "thing happens");
      #endregion
      //building hazard dictionary
      #region 

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
      //context commands
      #region
      if (input.Contains("use") && input.Contains("hammer") && input.Contains("lock"))
      {
        return gameActions.useHammerOnLock;
      };
      if (input.Contains("take") && input.Contains("hammer"))
      {
        return gameActions.takeHammer;
      }
      #endregion

      return gameActions.wait;
    }
    public void GetUserInput()
    {
      //assigning user input
      #region 
      String rawUserInput = Console.ReadLine();
      gameActions action = Parse(rawUserInput);

      #endregion
      switch (action)
      {
        //navigation
        #region
        case gameActions.goNorth:
          Go("north");
          break;
        case gameActions.goEast:
          Go("east");
          break;
        case gameActions.goSouth:
          Go("south");
          break;
        case gameActions.goWest:
          Go("west");
          break;
        case gameActions.goDown:
          Go("down");
          break;
        case gameActions.goUp:
          Go("up");
          break;
        case gameActions.goNorthWest:
          Go("northwest");
          break;
        case gameActions.goNorthEast:
          Go("northeast");
          break;
        case gameActions.goSouthEast:
          Go("southeast");
          break;
        case gameActions.goSouthWest:
          Go("southwest");
          break;
        #endregion
        //console commands
        #region
        case gameActions.help:
          Help();
          break;
        case gameActions.playerInventory:
          Inventory();
          break;
        case gameActions.gameQuit:
          Quit();
          break;
        #endregion
        //context commands
        #region
        case gameActions.useHammerOnLock:
          UseItem("worn hammer", "lock");
          break;
        case gameActions.takeHammer:
          TakeItem("worn hammer");
          break;
        case gameActions.lookRoom:
          Look();
          break;
          #endregion
      }
    }

    //command methods
    #region
    public void Go(string direction)
    {
      if (!CurrentRoom.Exits.ContainsKey(direction))
      {
        System.Console.WriteLine("There's Nothing in that Direction");
        return;
      };
      if (CurrentRoom.Exits[direction].Locked == true)
      {
        System.Console.WriteLine("its locked");
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


    public void Clear()
    {
      Console.Clear();
    }
    public void Intro()
    {
      System.Console.WriteLine("");
    }
    public void TakeItem(string itemName)
    {
      Item targetItem = CurrentRoom.Items.Find(i =>
      {
        return i.Name.ToLower() == itemName;
      });
      CurrentPlayer.Inventory.Add(targetItem);
      CurrentRoom.Items.Remove(targetItem);
      System.Console.WriteLine($"You Pickup the {targetItem.Name}");
    }

    public void UseItem(string itemName, string targetName)
    {
      string thing = $"{itemName}-{targetName}";
      if (!ItemUseGuide.ContainsKey(thing))
      {
        System.Console.WriteLine("I can't do that");
      };
      switch (thing)
      {
        case "worn hammer-lock":
          if (CurrentRoom.Name == "Small Bunker Entrance")
          {
            CurrentRoom.Exits["down"].Locked = false;
            System.Console.WriteLine("you break the lock");
          }
          else
          {
            System.Console.WriteLine("I cant do that");
            return;
          }
          Go("down");
          break;
      }

    }
    public void YouDied(string hazard)
    {
      switch (hazard)
      {

      }
    }
    #endregion
  }
}