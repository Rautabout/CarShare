using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.DAL.Entities
{
    class Bid
    {
        #region Fields
        public sbyte? BidID { get; set; }
        public sbyte? UserID { get; set; }
        public sbyte? VehicleID { get; set; }
        public int CurrentBid { get; set; }

        #endregion

        #region Constructors
        public Bid(MySqlDataReader reader)
        {
            BidID = sbyte.Parse(reader["bidId"].ToString());
            UserID = sbyte.Parse(reader["userId"].ToString());
            VehicleID = sbyte.Parse(reader["vehicleId"].ToString());
            CurrentBid = int.Parse(reader["currentBid"].ToString());

        }
        public Bid(sbyte userId,sbyte vehicleId,int currentBid)
        {
            BidID = null;
            UserID = userId;
            VehicleID = vehicleId;
            CurrentBid = currentBid;
        }

        public Bid(Bid bid)
        {
            BidID = bid.BidID;
            UserID = bid.UserID;
            VehicleID = bid.VehicleID;
            CurrentBid = bid.CurrentBid;
        }
        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"('{UserID}', '{VehicleID}','{CurrentBid}')";
        }

        #endregion
    }
}
