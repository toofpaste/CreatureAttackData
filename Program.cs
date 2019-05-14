using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

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
          // case "report": while(!GetCreatureReport()) {}
          // break;
        }
      }
    }


    static bool GetCreatureReport()
    {
      string input = "";
      Console.WriteLine("enter creature name");
      Console.Write("CreatureReport ▷");
      input = Console.ReadLine();
      input.ToLower();

      if (input == "q")
      {
        return true;
      }
      if (!DB.CreatureExist(input))
      {
        return false;
      }
      else
      {
        Creature creature = DB.GetCreatureByName(input);
        List<Dictionary<string, string>> reports = DB.GetDetailedAccountOfRampageAndCreatureIncident(creature.Id);
        foreach (Dictionary<string, string> report in reports) {
          Console.WriteLine("----------------------------------");
          Console.WriteLine(report["creatureName"]);
          Console.WriteLine(report["type"]);
          Console.WriteLine(report["status"]);
          Console.WriteLine(report["date"]);
          Console.WriteLine(report["damages"]);
          Console.WriteLine(report["cityName"]);
          Console.WriteLine(report["population"]);
        }
        return true;
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
        string Name = "";
        using (var db = new CADContext())
        {
          var creature = new Creature {Name = stats[0], Threat_level = int.Parse(stats[1]), Type = stats[2]};
          db.creature.Add(creature);
          db.SaveChanges();
          Name = creature.Name;
        }

        Console.WriteLine("{0} Added", Name);
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
      using (var db = new CADContext())
      {
        var creatures = db.creature.ToList();
        foreach (Creature creature in creatures) {
          Console.WriteLine("{0} {1} {2}", creature.Name, creature.Threat_level, creature.Type);
        }
      }
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
        string Name = "";
        using (var db = new CADContext())
        {
          var city = new City {Name = stats[0], Population = int.Parse(stats[1])};
          db.city.Add(city);
          db.SaveChanges();
          Name = city.Name;
        }
        Console.WriteLine("{0} Added", Name);
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

      if (input == "q") { return true; }
      string[] stats = input.Split();

      using (var db = new CADContext())
      {
        var creature = db.creature.Where(b => b.Name == stats[0]).FirstOrDefault();
        var city = db.city.Where(b => b.Name == stats[1]).FirstOrDefault();

        if (creature == null)
        {
          Console.WriteLine("Creature not registered to CAD");
          return false;
        }

        if (city == null)
        {
          Console.WriteLine("City not registered to CAD");
          return false;
        }

        try {
          DateTime date = new DateTime(int.Parse(stats[4]), int.Parse(stats[2]), int.Parse(stats[3]));
          Rampage rampage = new Rampage {Creature_id = creature.Id, City_id = city.Id, Status = "Ongoing", Date = date, Damages = 0};
          db.rampage.Add(rampage);
          db.SaveChanges();
          Console.WriteLine("Rampage Logged as an ongoing event");
          return true;
        }
        catch
        {
          Console.WriteLine("ERROR: Incorrect Values");
          return false;
        }
      }
    }

    static void PrintAllCities()
    {
      using (var db = new CADContext())
      {
        var cities = db.city.ToList();
        Console.WriteLine("all targeted cities: ");
        foreach (City city in cities) {
          Console.WriteLine("{0} {1}", city.Name, city.Population);
        }
      }
    }

  }
}
