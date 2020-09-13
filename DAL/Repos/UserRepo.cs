using CarShare.DAL.Entities;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace CarShare.DAL.Repos
{
    class UserRepo
    {
        #region SQL_QUERIES
        private const string ALL_USERS = "SELECT * FROM users";
        private const string ADD_USER = "INSERT INTO `users`(`UserName`,`Password`) VALUES ";
        private const string DELETE_USER = "DELETE FROM `users` WHERE UserID=";
        private const string DELETE_USER_BIDS = "DELETE FROM `bids` WHERE UserID=";

        #endregion

        #region CRUD

        public static List<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand(ALL_USERS, connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new User(reader));
                }
                connection.Close();
            }
            return users;
        }

        public static bool AddUserToDB(User user)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"{ADD_USER} {user.ToInsert()}", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var id = command.ExecuteNonQuery();
                status = true;
                user.UserID = (sbyte)command.LastInsertedId;
                connection.Close();
            }
            return status;
        }

        public static bool EditUser(User user, sbyte userID)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command = new MySqlCommand($"UPDATE users SET UserName='{user.UserName}', Password='{user.Password}' WHERE UserID={userID};", connection);
                try { connection.Open(); }
                catch { MessageBox.Show("Error connecting with database!"); Application.Current.Shutdown(); }
                var n = command.ExecuteNonQuery();
                if (n == 1)
                {
                    status = true;
                }
                connection.Close();
            }

            return status;
        }

        public static bool DeleteUser(sbyte userID)
        {
            bool status = false;
            using (var connection = DBConnection.Instance.Connection)
            {
                MySqlCommand command1 = new MySqlCommand($"{DELETE_USER} {userID}", connection);
                MySqlCommand command2 = new MySqlCommand($"{DELETE_USER_BIDS} {userID}", connection);

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
