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
        if (input == "add creature")
        {
          while(!CreateCreature()) {}
        }
      }


    }

    static bool CreateCreature()
    {
      string input = "";
      Console.WriteLine("enter creature details (name, threat level, type)");
      Console.Write("▷");
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
  }
}
