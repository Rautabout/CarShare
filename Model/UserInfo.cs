using CarShare.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarShare.Model
{
    class UserInfo
    {
        private static UserInfo _instance = null;
        public static UserInfo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new UserInfo();
                }
                return _instance;
            }
        }

        private User _currentUser;

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                this._currentUser = value;
            }
        }
        

        public sbyte currentUser { get; set; }// = (sbyte)userInfo.CurrentUser.UserID;


        private UserInfo() { }
    }
}
