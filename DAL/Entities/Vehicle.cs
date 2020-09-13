using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using Ubiety.Dns.Core.Common;

namespace CarShare.DAL.Entities
{
    class Vehicle
    {
        #region Fields
        public sbyte? VehicleID { get; set; }
        public string Maker { get; set; }

        public string Model { get; set; }
        public string Version { get; set; }
        public string Engine { get; set; }
        public int Power { get; set; }
        public int ModelYear { get; set; }
        public int HighestBid { get; set; }
        public sbyte? CurrentOwner { get; set; }
        public bool IsForSale { get; set; }


        #endregion

        #region Constructors
        public Vehicle(MySqlDataReader reader)
        {
            VehicleID = sbyte.Parse(reader["vehicleId"].ToString());
            Maker = reader["maker"].ToString();
            Model = reader["model"].ToString();
            Version = reader["version"].ToString();
            Engine = reader["engine"].ToString();
            Power = int.Parse(reader["power"].ToString());
            ModelYear = int.Parse(reader["modelYear"].ToString());
            HighestBid = int.Parse(reader["highestBid"].ToString());
            CurrentOwner = sbyte.Parse(reader["currentOwner"].ToString());
            IsForSale = bool.Parse(reader["isForSale"].ToString());
        }

        public Vehicle(string maker, string model, string version, string engine, int power, int modelYear, int highestBid, sbyte currentOwner, bool isForSale)
        {
            VehicleID = null;
            Maker = maker.Trim();
            Model = model.Trim();
            Version = version.Trim();
            Engine = engine.Trim();
            Power = power;
            ModelYear = modelYear;
            HighestBid = highestBid;
            CurrentOwner = currentOwner;
            IsForSale = isForSale;
        }

        public Vehicle(Vehicle vehicle)
        {
            VehicleID = vehicle.VehicleID;
            Maker = vehicle.Maker;
            Model = vehicle.Model;
            Version = vehicle.Version;
            Engine = vehicle.Engine;
            Power = vehicle.Power;
            ModelYear = vehicle.ModelYear;
            HighestBid = vehicle.HighestBid;
            CurrentOwner = vehicle.CurrentOwner;
            IsForSale = vehicle.IsForSale;
        }
        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"('{Maker}', '{Model}','{Version}','{Engine}','{Power}','{ModelYear}','{1}','{CurrentOwner}','{1}')";
        }
        #endregion
    }
}
