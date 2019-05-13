using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace Rampage
{
  public partial class DB
  {
    public static City CreateCity(string name, int population)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO city (name, population) VALUES (@name, @population);";
      cmd.Parameters.AddWithValue("@name", name);
      cmd.Parameters.AddWithValue("@population", population);
      cmd.ExecuteNonQuery();
      DB.Close(conn);

      return new City() {
        Id = cmd.LastInsertedId,
        Name = name,
        Population = population
      };
    }

    public static List<City> GetAllCitys()
    {
      List<City> cities = new List<City>();
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM city;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        City newCity = new City() {
          Id = rdr.GetInt32(0),
          Name = rdr.GetString(1),
          Population = rdr.GetInt32(2)
        };

        cities.Add(newCity);
      }
      return cities;
    }

    public static City GetCityByName(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM city WHERE name = @name;";
      cmd.Parameters.AddWithValue("@name", name);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      rdr.Read()
      City newCity = new City() {
        Id = rdr.GetInt32(0),
        Name = rdr.GetString(1),
        Population = rdr.GetInt32(2)
      };
      return newCity;
    }

    public static bool CityExist(string name)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM city WHERE name = @name;";
      cmd.Parameters.AddWithValue("@name", name);
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      return rdr.Read();
    }

  }
}
