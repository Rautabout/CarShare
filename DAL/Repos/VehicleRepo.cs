using CarShare.DAL.Entities;
using CarShare.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CarShare.DAL.Repos
{
    class VehicleRepo
    {
        #region SQL_QUERIES
        private const string ALL_VEHICLES = "SELECT * FROM vehicles";
        private const string ALL_USER_VEHICLES = "SELECT * FROM vehicles WHERE CurrentOwner=";

        //private const string ADD_USER = "INSERT INTO `users`(`UserName`,`Password`) VALUES ";
        //private const string DELETE_USER = "DELETE FROM `users` WHERE UserID=";
        //private const string DELETE_USER_BIDS = "DELETE FROM `bids` WHERE UserID=";

        #endregion

        #region CRUD

        public static List<Vehicle> GetAllVehicles()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_VEHICLES, connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    vehicles.Add(new Vehicle(reader));
                }
                connection.Close();
            }
            return vehicles;
        }

        public static List<Vehicle> GetAllUserVehicles(sbyte currentUser)
        {
            UserInfo user;
            List<Vehicle> vehicles = new List<Vehicle>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ALL_USER_VEHICLES} {currentUser}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    vehicles.Add(new Vehicle(reader));
                }
                connection.Close();
            }
            return vehicles;
        }

        #endregion
    }
}
