using CarShare.ViewModel.BaseClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using CarShare.DAL.Entities;
    using CarShare.DAL.Repos;
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Windows.Input;

    class ChangePasswordViewModel : ViewModelBase
    {
        #region PrivateConstructor
        private DBConnection dBConnection;
        private Model model = null;
        private string oldPassword, newPassword;
        #endregion

        #region Constructors

        public ChangePasswordViewModel(Model model)
        {
            this.model = model;
            Users = model.Users;
        }

        #endregion

        #region ViewChanger
        public ChangePasswordViewModel()
        {
            dBConnection = new DBConnection();
        }
        #endregion

        #region Propertes
        public ObservableCollection<User> Users { get; set; }

        public string OldPassword
        {
            get => oldPassword;
            set
            {
                oldPassword = value;
                onPropertyChanged(nameof(OldPassword));
            }
        }

        public string NewPassword
        {
            get => newPassword;
            set
            {
                newPassword = value;
                onPropertyChanged(nameof(NewPassword));
            }
        }

        private void ClearFormula()
        {
            OldPassword = "";
            NewPassword = "";
        }
        #endregion

        #region Commands

        private ICommand changePassword = null;
        public ICommand ChangePassword
        {
            get
            {
                if (changePassword == null)
                {
                    changePassword = new RelayCommand(arg =>
                      {
                          var userInfo = UserInfo.Instance;
                          var user = userInfo.CurrentUser;
                          if (user.Password == OldPassword)
                          {
                              user.Password = NewPassword;
                              if (user.UserID != null)
                              {
                                  UserRepo.EditUser(user, (sbyte)user.UserID);
                                  MessageBox.Show("Password changed!");

                              }
                              else
                              {
                                  MessageBox.Show("Wrong credentials!");
                                  ClearFormula();
                              }
                          }
                      }, arg => (OldPassword != "") && (NewPassword != ""));
                }
                return changePassword;

            }
        }

        #endregion
    }
}
