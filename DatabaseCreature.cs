using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Rampage
{
  public partial class DB
  {
    public static Creature CreateCreature(string name, int threat_level, string type)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO creature (name, threat_level, type) VALUES (@name, @threat_level, @type);";
      cmd.Parameters.AddWithValue("@name", name);
      cmd.Parameters.AddWithValue("@threat_level", threat_level);
      cmd.Parameters.AddWithValue("@type", type);
      cmd.ExecuteNonQuery();
      DB.Close(conn);

      return new Creature() {
        Id = cmd.LastInsertedId,
        Name = name,
        Threat_level = threat_level,
        Type = type
      };
    }

    public static List<Creature> GetAllCreatures()
    {
      List<Creature> creatures = new List<Creature>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM creature;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        Creature newCreature = new Creature() {
          Id = rdr.GetInt32(0),
          Name = rdr.GetString(1),
          Threat_level = rdr.GetInt32(2),
          Type = rdr.GetString(3)
        };

        creatures.Add(newCreature);
      }
      return creatures;
    }

    public static Creature GetCreatureByName(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM creature WHERE name = @name;";
      cmd.Parameters.AddWithValue("@name", name);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      rdr.Read();
      Creature newCreature = new Creature() {
        Id = rdr.GetInt32(0),
        Name = rdr.GetString(1),
        Threat_level = rdr.GetInt32(2),
        Type = rdr.GetString(3)
      };
      return newCreature;
    }

    public static bool CreatureExist(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM creature WHERE name = @name;";
      cmd.Parameters.AddWithValue("@name", name);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      return rdr.Read();
    }

  }
}
