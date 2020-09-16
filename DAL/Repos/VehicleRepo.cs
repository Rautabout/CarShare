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
        private const string ALL_VEHICLES_BY_ID = "SELECT * FROM vehicles WHERE VehicleID=";
        private const string ADD_VEHICLE = "INSERT INTO `vehicles`(`Maker`, `Model`, `Version`, `Engine`, `Power`, `ModelYear`,`CurrentOwner`) VALUES ";
        private const string DELETE_VEHICLE= "DELETE FROM `vehicles` WHERE VehicleID=";
        private const string DELETE_VEHICLE_BIDS = "DELETE FROM `bids` WHERE VehicleID=";

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

        public static List<Vehicle> GetAllVehiclesByID(sbyte vehicleID)
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ALL_VEHICLES_BY_ID} {vehicleID}", connection);
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


        public static bool AddVehicleToDB(Vehicle vehicle)
        {
            bool status = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_VEHICLE} {vehicle.ToInsert()}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var id = command.ExecuteNonQuery();
                status = true;
                vehicle.VehicleID = (sbyte)command.LastInsertedId;
                connection.Close();
            }

            return status;
        }

        public static bool EditVehicleInDB(Vehicle vehicle, sbyte vehicleID)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_VEHICLE = $"UPDATE `vehicles` SET `Maker`='{vehicle.Maker}', `Model`='{vehicle.Model}', " +
                                           $"`Version`='{vehicle.Version}', `Engine`='{vehicle.Engine}'," +
                                           $"`Power`='{vehicle.Power}', `ModelYear`='{vehicle.ModelYear}'"+
                                           $" WHERE (`VehicleID`='{vehicleID}');";

                MySqlCommand command = new MySqlCommand(EDIT_VEHICLE, connection);
                //MessageBox.Show(EDIT_VEHICLE);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var n = command.ExecuteNonQuery();
                if (n == 1) status = true;

                connection.Close();
            }
            return status;
        }

        public static bool DeleteVehicleInDB(object idVehicle)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                MySqlCommand command1 = new MySqlCommand($"{DELETE_VEHICLE} {idVehicle}", connection);
                MySqlCommand command2 = new MySqlCommand($"{DELETE_VEHICLE_BIDS} {idVehicle}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                command1.ExecuteNonQuery();
                command2.ExecuteNonQuery();
                status = true;
                connection.Close();
            }
            return status;
        }


        #endregion
    }
}
