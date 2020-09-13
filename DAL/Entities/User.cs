using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShare.DAL.Entities
{
    class User
    {
        #region Fields
        public sbyte? UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        #endregion

        #region Constructors
        public User(MySqlDataReader reader)
        {
            UserID = sbyte.Parse(reader["userId"].ToString());
            UserName = reader["username"].ToString();
            Password = reader["password"].ToString();
        }

        public User(string username, string password)
        {
            UserID = null;
            UserName = username.Trim();
            Password = password.Trim();
        }

        public User(User user)
        {
            UserID = user.UserID;
            UserName = user.UserName;
            Password = user.Password;
        }
        #endregion

        #region Methods

        public string ToInsert()
        {
            return $"('{UserName}', '{Password}')";
        }

        public override bool Equals(object obj)
        {
            //No ID comparison
            var user = obj as User;
            if (user is null) return false;
            if (UserName.ToLower() != user.UserName.ToLower()) return false;
            if (Password.ToLower() != user.Password.ToLower()) return false;
            return true;
        }

        //Hashcode download
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion
    }
}
