using CarShare.DAL.Entities;
using CarShare.DAL.Repos;
using Org.BouncyCastle.Bcpg.OpenPgp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace CarShare.Model
{
    class Model
    {
        #region DBCollections
        public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

        public ObservableCollection<Vehicle> Vehicles { get; set; } = new ObservableCollection<Vehicle>();


        public ObservableCollection<Bid> Bids { get; set; } = new ObservableCollection<Bid>();


        #endregion

        #region Model
        public Model()
        {
            var users = UserRepo.GetAllUsers();
            var vehicles = VehicleRepo.GetAllVehicles();
            var bids = BidRepo.GetAllBids();

            foreach (var u in users)
            {
                Users.Add(u);
            }
            foreach(var v in vehicles)
            {
                Vehicles.Add(v);
            }
            
            foreach (var b in bids)
            {
                Bids.Add(b);
            }
        }
        #endregion

        #region SearchForLoginInDB
        public User SearchUserByUserName(string userName)
        {
            return Users.FirstOrDefault(u => u.UserName == userName);
        }
        #endregion

        #region AddingUserToDB
        public bool IfUserInDB(User user) => Users.Contains(user);
        public bool AddUser(User user)
        {
            if (IfUserInDB(user))
            {
                return false;
            }
            if (!UserRepo.AddUserToDB(user))
            {
                return false;
            }
            Users.Add(user);
            return true;
        }

        #endregion

        #region DeletingUserFromDB
        public bool DeleteUser(sbyte userID)
        {
            if (!UserRepo.DeleteUser(userID))
            {
                return false;
            }
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserID != userID)
                {
                    continue;
                }
                Users.RemoveAt(i);
            }
            return true;
        }
        #endregion

        #region EditingUserInDB
        public bool EditUserInDB(User user, sbyte userID)
        {
            if (!UserRepo.EditUser(user, userID))
            {
                return false;
            }
            for (int i = 0; i < Users.Count; i++)
            {
                if (Users[i].UserID != userID)
                {
                    continue;
                }
                user.UserID = userID;
                Users[i] = new User(user);
            }
            return true;
        }
        #endregion

        #region AddVehicleToDB
        public bool IfVehicleInDB(Vehicle vehicle) => Vehicles.Contains(vehicle);
        public bool AddVehicle(Vehicle vehicle)
        {
            if (IfVehicleInDB(vehicle)) return false;
            if (!VehicleRepo.AddVehicleToDB(vehicle)) return false;
            Vehicles.Add(vehicle);
            return true;
        }
        #endregion

        #region EditVehicleInDB
        public bool EditVehicleInDB(Vehicle vehicle, sbyte idVehicle)
        {
            if (!VehicleRepo.EditVehicleInDB(vehicle, idVehicle)) return false;
            for (int i = 0; i < Vehicles.Count; i++)
            {
                if (Vehicles[i].VehicleID != idVehicle) continue;
                vehicle.VehicleID = idVehicle;
                Vehicles[i] = new Vehicle(vehicle);
            }

            return true;
        }

        #endregion

        #region RemoveVehicleFromDB
        public bool RemoveVehicleFromDb(sbyte idVehicle)
        {
            if (!VehicleRepo.DeleteVehicleInDB(idVehicle)) return false;
            for (int i = 0; i < Vehicles.Count; i++)
            {
                if (Vehicles[i].VehicleID != idVehicle) continue;
                Vehicles.RemoveAt(i);
            }
            return true;
        }


        #endregion

        #region AddBidToDB
        public bool IfBidInDB(Bid bid) => Bids.Contains(bid);
        public bool AddBid(Bid bid)
        {
            if (IfBidInDB(bid))
            {
                return false;
            }
            if (!BidRepo.AddBidToDB(bid))
            {
                return false;
            }
            Bids.Add(bid);
            return true;
        }
        #endregion

        #region RemoveBidFromDB
        public bool RemoveBidFromDb(sbyte idBid)
        {
            if (!BidRepo.DeleteBidInDB(idBid)) return false;
            for (int i = 0; i < Bids.Count; i++)
            {
                if (Bids[i].BidID != idBid) continue;
                Bids.RemoveAt(i);
            }
            return true;
        }
        #endregion

        #region Logged

        public User Logged { get; set; }

        



        #endregion
    }
}
