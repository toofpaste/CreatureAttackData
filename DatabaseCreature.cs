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
  }
}
