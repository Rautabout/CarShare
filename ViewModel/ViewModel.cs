using System;
using System.Collections.Generic;
using System.Text;

namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using CarShare.ViewModel.BaseClass;
    using Model;
    using System.Windows.Input;

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


        

        private static Model model = new Model();

        public LoginViewModel loginViewModel { get; set; }

        public VehiclesViewModel vehiclesViewModel { get; set; }

        public ViewModel()
        {
            vehiclesViewModel = new VehiclesViewModel(model);
            loginViewModel = new LoginViewModel(model);
            UpdateViewCommand = new UpdateViewCommand(this);
        }


    }
}
