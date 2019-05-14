using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Rampage
{

  public class CADContext : DbContext
  {
    public DbSet<City> city { get; set; }
    public DbSet<Creature> creature { get; set; }
    public DbSet<Rampage> rampage { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseMySQL("server=localhost;database=creature_attacks_database;user=root;password=root;port=8889;");
    }

  }

  public class City
  {
    public long Id {get; set;}
    public string Name {get; set;}
    public int Population {get; set;}
  }

  public class Creature
  {
    public long Id {get; set;}
    public string Name {get; set;}
    public int Threat_level {get; set;}
    public string Type {get; set;}
  }

  public class Rampage
  {
    public long Id {get; set;}
    public long Creature_id {get; set;}
    public long City_id {get; set;}
    public string Status {get; set;}
    public DateTime Date {get; set;}
    public int Damages {get; set;}
  }
}
