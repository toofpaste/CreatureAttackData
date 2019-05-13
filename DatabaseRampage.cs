using System;
using System.Diagnostics;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Rampage
{
  public partial class DB
  {
    public static Rampage CreateRampage(long creature_id, long city_id, string status, DateTime date, int damages)
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
        Status = status,
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
          Status = rdr.GetString(3),
          Date = rdr.GetDateTime(4),
          Damages = rdr.GetInt32(5)
        };

        rampages.Add(newRampage);
      }
      return rampages;
    }

    public static List<Dictionary<string, string>> GetDetailedAccountOfRampageAndCreatureIncident(long id)
    {
      List<Dictionary<string, string>> rampages = new List<Dictionary<string, string>> ();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM creature
      JOIN rampage ON (creature.id = rampage.creature_id)
      JOIN city ON (rampage.city_id = city.id)
      WHERE creature.id = @id;";
      cmd.Parameters.AddWithValue("@id", id);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        Dictionary<string, string> newRampage = new Dictionary<string, string> ();
        newRampage.Add("creatureName", rdr["name"].ToString());
        newRampage.Add("type", rdr["type"].ToString());
        newRampage.Add("status", rdr["status"].ToString());
        newRampage.Add("date", rdr["date"].ToString());
        newRampage.Add("damages", rdr["damages"].ToString());
        newRampage.Add("cityName", rdr.GetString(11));
        newRampage.Add("population", rdr["population"].ToString());
        rampages.Add(newRampage);
      }
      return rampages;
    }

  }
}
