using System;

namespace Rampage
{
  class Program
  {
    static void Main(string[] args)
    {
      Console.Clear();
      Console.WriteLine("Hello Agent 33924");
      Console.WriteLine("CAD (Creature Attack Database)");
      bool quit = false;
      while (!quit)
      {
        string input = "";
        while (input == "")
        {
          Console.Write("▷");
          input = Console.ReadLine();
          input.ToLower();
        }


        if (input == "q" || input == "quit" || input == "exit")
        {
          quit = true;
        }
        switch (input) {
          case "add creature": while(!CreateCreature()) {}
          break;
          case "add city": while(!CreateCity()) {}
          break;
          case "help":
          case "?": Help();
          break;
          case "creatures": PrintAllCreatures();
          break;
          case "cities": PrintAllCities();
          break;
          case "add rampage": while(!CreateRampage()) {}
          break;
        }
      }
    }


    static void Help()
    {
      Console.WriteLine("'add creature': adds a creature to CAD");
      Console.WriteLine("'add city': adds a city to CAD");
      Console.WriteLine("'add rampage': adds a rampage incident");
      Console.WriteLine("'creatures': lists all creatures");
      Console.WriteLine("'cities': lists all cities");
    }

    static bool CreateCreature()
    {
      string input = "";
      Console.WriteLine("enter creature details (name, threat level 0-?, type)");
      Console.Write("Creature ▷");
      input = Console.ReadLine();
      input.ToLower();

      if (input == "q")
      {
        return true;
      }
      string[] stats = input.Split();
      try {
        Creature newCreature = DB.CreateCreature(stats[0], int.Parse(stats[1]) , stats[2]);
        Console.WriteLine("{0} Added", newCreature.Name);
        return true;
      }
      catch
      {
        Console.WriteLine("ERROR: Incorrect Values");
        return false;
      }
    }

    static void PrintAllCreatures()
    {
      Console.WriteLine("all known creatures: ");
      foreach (Creature creature in DB.GetAllCreatures())
      Console.WriteLine("{0} {1} {2}", creature.Name, creature.Threat_level, creature.Type);
    }

    static bool CreateCity()
    {
      string input = "";
      Console.WriteLine("enter targeted city details (name, population)");
      Console.Write("City ▷");
      input = Console.ReadLine();
      input.ToLower();

      if (input == "q")
      {
        return true;
      }
      string[] stats = input.Split();
      try {
        City newCity = DB.CreateCity(stats[0], int.Parse(stats[1]));
        Console.WriteLine("{0} Added", newCity.Name);
        return true;
      }
      catch
      {
        Console.WriteLine("ERROR: Incorrect Values");
        return false;
      }
    }

    static bool CreateRampage()
    {
      string input = "";
      Console.WriteLine("enter rampage details (creature, city, date MM DD YYYY)");
      Console.Write("Rampage ▷");
      input = Console.ReadLine();
      input.ToLower();

      if (input == "q")
      {
        return true;
      }
      string[] stats = input.Split();
      if (!DB.CreatureExist(stats[0]))
      {
        Console.WriteLine("Creature not registered to CAD");
        return false;
      }
      if (!DB.CityExist(stats[1]))
      {
        Console.WriteLine("City not registered to CAD");
        return false;
      }
      Creature creature = DB.GetCreatureByName(stats[0]);
      City city = DB.GetCityByName(stats[1]);

      try {
        DateTime date = new DateTime(int.Parse(stats[4]), int.Parse(stats[2]), int.Parse(stats[3]));

        Rampage newRampage = DB.CreateRampage(creature.Id, city.Id, "Ongoing", date, 0);
        Console.WriteLine("Rampage Logged as an ongoing event");
        return true;
      }
      catch
      {
        Console.WriteLine("ERROR: Incorrect Values");
        return false;
      }
    }

    static void PrintAllCities()
    {
      Console.WriteLine("all targeted cities: ");
      foreach (City city in DB.GetAllCitys())
      Console.WriteLine("{0} {1}", city.Name, city.Population);
    }

  }
}
