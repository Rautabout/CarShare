using CarShare.DAL.Entities;
using CarShare.Model;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CarShare.DAL.Repos
{
    class BidRepo
    {
        #region SQL_QUERIES
        private const string ALL_BIDS = "SELECT * FROM bids";
        private const string ADD_BID = "INSERT INTO `bids`(`UserID`,`VehicleID`,`CurrentBid`) VALUES ";
        private const string DELETE_BIDS = "DELETE FROM `bids` WHERE BidID=";
        private const string DELETE_USER_BIDS = "DELETE FROM `bids` WHERE UserID=";

        #endregion

        #region CRUD
        public static List<Bid> GetAllBids()
        {
            List<Bid> bids = new List<Bid>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_BIDS, connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    bids.Add(new Bid(reader));
                }
                connection.Close();
            }
            return bids;
        }
        public static bool AddBidToDB(Bid bid)
        {
            bool status = false;

            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_VEHICLE = $"UPDATE `vehicles` SET `HighestBid`='{bid.CurrentBid}'" +
                                           $" WHERE (`VehicleID`='{bid.VehicleID}');";
                MySqlCommand command1 = new MySqlCommand($"{ADD_BID} {bid.ToInsert()}", connection);
                MySqlCommand command2 = new MySqlCommand($"{EDIT_VEHICLE}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var id = command1.ExecuteNonQuery();
                var n = command2.ExecuteNonQuery();
                status = true;
                bid.BidID = (sbyte)command1.LastInsertedId;
                connection.Close();
            }

            return status;
        }
        public static bool DeleteBidInDB(object idBid)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {

                MySqlCommand command = new MySqlCommand($"{DELETE_BIDS} {idBid}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                command.ExecuteNonQuery();
                status = true;
                connection.Close();
            }
            return status;
        }
        public static bool EditBidInDB(Bid bid, sbyte bidId)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                string EDIT_BID = $"UPDATE `bids` SET `CurrentBid`='{bid.CurrentBid}'" +
                                           $" WHERE (`BidID`='{bidId}');";
                string EDIT_VEHICLE = $"UPDATE `vehicles` SET `HighestBid`='{bid.CurrentBid}'" +
                                           $" WHERE (`VehicleID`='{bid.VehicleID}');";
                MySqlCommand command1 = new MySqlCommand($"{EDIT_BID}", connection);
                MySqlCommand command2 = new MySqlCommand($"{EDIT_VEHICLE}", connection);

                //MessageBox.Show(EDIT_VEHICLE);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var id = command1.ExecuteNonQuery();
                var n = command2.ExecuteNonQuery();
                status = true;

                connection.Close();
            }
            return status;
        }

        #endregion



    }
}
