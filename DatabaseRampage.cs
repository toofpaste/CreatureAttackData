using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Rampage
{
  public partial class DB
  {
    public static Rampage CreateRampage(long creature_id, int city_id, string status, DateTime date, int damages)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO rampage (creature_id, city_id, status, date, damages) VALUES (@creature_id, @city_id, @status, @date, @damages);";
      cmd.Parameters.AddWithValue("@creature_id", creature_id);
      cmd.Parameters.AddWithValue("@city_id", city_id);
      cmd.Parameters.AddWithValue("@status", status);
      cmd.Parameters.AddWithValue("@date", date);
      cmd.Parameters.AddWithValue("@damages", damages);

      cmd.ExecuteNonQuery();
      DB.Close(conn);

      return new Rampage() {
        Id = cmd.LastInsertedId,
        Creature_id = creature_id,
        City_id = city_id,
        Stats = status,
        Date = date,
        Damages = damages
      };
    }

    public static List<Rampage> GetAllRampages()
    {
      List<Rampage> rampages = new List<Rampage>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM rampage;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        Rampage newRampage = new Rampage() {
          Id = rdr.GetInt32(0),
          Creature_id = rdr.GetInt32(1),
          City_id = rdr.GetInt32(2),
          Stats = rdr.GetString(3),
          Date = rdr.GetDateTime(4),
          Damages = rdr.GetInt32(5)
        };

        rampages.Add(newRampage);
      }
      return creatures;
    }

  }
}
