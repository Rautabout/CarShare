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
        private const string ALL_USER_BIDS = "SELECT * FROM bids WHERE UserID=";
        private const string ADD_BID = "INSERT INTO `bids`(`UserID`,`VehicleID`,`CurrentBid`) VALUES ";
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

        public static List<Bid> GetAllUserBids(sbyte currentUser)
        {
            List<Bid> bids = new List<Bid>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ALL_USER_BIDS} {currentUser}", connection);
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
        #endregion


      
    }
}
