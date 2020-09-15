using System;
using System.Collections.Generic;
using System.Text;

namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using CarShare.ViewModel.BaseClass;
    using Model;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Markup;

    class ViewModel : ViewModelBase
    {

        #region ButtonViewChanger
        private ViewModelBase _selectedViewModel;

        public ViewModelBase SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                onPropertyChanged(nameof(SelectedViewModel));
            }
        }
        public ICommand UpdateViewCommand { get; set; }
        #endregion

        #region ModelDeclaration
        private static Model model = new Model();
        #endregion

        #region ViewModels Declaration

        public LoginViewModel loginViewModel { get; set; }
        public ChangePasswordViewModel changePasswordViewModel { get; set; }

        public VehiclesViewModel vehiclesViewModel { get; set; }
        public MyVehiclesViewModel myVehiclesViewModel { get; set; }
        #endregion

        #region Initializing Constructors

        public ViewModel()
        {

            vehiclesViewModel = new VehiclesViewModel(model);
            loginViewModel = new LoginViewModel(model);
            changePasswordViewModel = new ChangePasswordViewModel(model);
            myVehiclesViewModel = new MyVehiclesViewModel(model);
            UpdateViewCommand = new UpdateViewCommand(this);
        }

        #endregion

    }
}
