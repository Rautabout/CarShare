using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CarShare.ViewModel.BaseClass
{
    class UpdateViewCommand : ICommand
    {
        private ViewModel viewModel;

        public event EventHandler CanExecuteChanged;
        public UpdateViewCommand(ViewModel viewModel)
        {
            this.viewModel = viewModel;
        }


        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if (parameter.ToString() == "Vehicles")
            {
                viewModel.SelectedViewModel = new VehiclesViewModel();
            }
            else if(parameter.ToString()=="ChangePassword")
            {
                viewModel.SelectedViewModel = new ChangePasswordViewModel();
            }
            else if (parameter.ToString() == "MyVehicles")
            {
                viewModel.SelectedViewModel = new MyVehiclesViewModel();
            }
        }
    }
}
