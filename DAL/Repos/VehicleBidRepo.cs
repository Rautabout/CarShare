using CarShare.DAL.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarShare.DAL.Repos
{
    class VehicleBidRepo
    {
        #region SQL_QUERIES
        private const string ALL_USER_BIDS_VEHICLES = "SELECT bids.BidID,bids.VehicleID,bids.UserID,vehicles.Maker,vehicles.Model,vehicles.Version,vehicles.Engine,vehicles.Power,vehicles.ModelYear,vehicles.HighestBid,bids.CurrentBid"+
            " FROM bids, vehicles"+
            " WHERE bids.VehicleID=vehicles.VehicleID and bids.UserID=";
        #endregion

        #region CRUD

        public static List<VehicleBid> GetAllUserVehicleBid(sbyte currentUser)
        {
            List<VehicleBid> vehicleBids = new List<VehicleBid>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ALL_USER_BIDS_VEHICLES} {currentUser}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    vehicleBids.Add(new VehicleBid(reader));
                }
                connection.Close();
            }
            return vehicleBids;
        }
        #endregion
    }
}
