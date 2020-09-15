using System.Windows.Input;
using CarShare.View;

namespace CarShare.ViewModel
{
    using BaseClass;
    using Model;
    using DAL.Entities;
    using System.Collections.ObjectModel;
    using System.Windows;
    using System.Configuration;

    class LoginViewModel : ViewModelBase
    {
        #region PrivateConstructor

        private Model model = null;
        private string username, password;

        #endregion

        #region Constructor

        public LoginViewModel(Model model)
        {
            this.model = model;
            Users = model.Users;
        }

        #endregion

        #region Propertes
        public ObservableCollection<User> Users { get; set; }

        public string UserName
        {
            get => username;
            set
            {
                username = value;
                onPropertyChanged(nameof(UserName));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                onPropertyChanged(nameof(Password));
            }
        }

        #endregion

        #region ClearFormula
        private void ClearFormula()
        {
            UserName = "";
            Password = "";
        }

        #endregion

        #region Commands


        private ICommand login = null;
        public ICommand Login
        {
            get
            {
                if (login == null)
                    login = new RelayCommand(
                        arg =>
                        {
                            var userInfo = UserInfo.Instance;
                            var user = model.SearchUserByUserName(UserName);
                            if (user != null && Password == user.Password)
                            {
                                model.Logged = user;
                                userInfo.CurrentUser = user;
                                userInfo.currentUser= (sbyte)userInfo.CurrentUser.UserID;
                                var mainWindow = new MainWindow();
                                mainWindow.Show();
                                var loginWindow = (LoginScreen)arg;
                                loginWindow.Close();
                            }
                            else
                            {
                                MessageBox.Show("Wrong Credentials!");
                                ClearFormula();
                            }
                        },
                        arg => (UserName != "" && Password != "")
                    );

                return login;
            }
        }

        #endregion
    }
}
