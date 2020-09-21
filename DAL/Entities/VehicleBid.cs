using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.DAL.Entities
{
    class VehicleBid
    {
        #region Fields
        public sbyte? BidID { get; set; }
        public sbyte? VehicleID { get; set; }
        public sbyte? UserID { get; set; }
        public string Maker { get; set; }
        public string Model { get; set; }
        public string Version { get; set; }
        public string Engine { get; set; }
        public int Power { get; set; }
        public int ModelYear { get; set; }
        public int HighestBid { get; set; }
        public int CurrentBid { get; set; }

        #endregion

        #region Constructors
        public VehicleBid(MySqlDataReader reader)
        {
            BidID = sbyte.Parse(reader["bidId"].ToString());
            UserID = sbyte.Parse(reader["vehicleId"].ToString());
            VehicleID = sbyte.Parse(reader["vehicleId"].ToString());
            Maker = reader["maker"].ToString();
            Model = reader["model"].ToString();
            Version = reader["version"].ToString();
            Engine = reader["engine"].ToString();
            Power = int.Parse(reader["power"].ToString());
            ModelYear = int.Parse(reader["modelYear"].ToString());
            HighestBid = int.Parse(reader["highestBid"].ToString());
            CurrentBid = int.Parse(reader["currentBid"].ToString());
        }


        public VehicleBid(sbyte userID,sbyte vehicleID,string maker, string model, string version, string engine, int power, int modelYear,int highestBid,int currentBid)
        {
            BidID = null;
            UserID = userID;
            VehicleID = vehicleID;
            Maker = maker.Trim();
            Model = model.Trim();
            Version = version.Trim();
            Engine = engine.Trim();
            Power = power;
            ModelYear = modelYear;
            HighestBid = highestBid;
            CurrentBid = currentBid;
        }
       

        public VehicleBid(VehicleBid vehicleBid)
        {
            BidID = null;
            UserID = vehicleBid.UserID;
            VehicleID = vehicleBid.VehicleID;
            Maker = vehicleBid.Maker;
            Model = vehicleBid.Model;
            Version = vehicleBid.Version;
            Engine = vehicleBid.Engine;
            Power = vehicleBid.Power;
            ModelYear = vehicleBid.ModelYear;
            HighestBid = vehicleBid.HighestBid;
            CurrentBid = vehicleBid.CurrentBid;

        }
        #endregion

    }
}
