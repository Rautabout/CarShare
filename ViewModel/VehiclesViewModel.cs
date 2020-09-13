namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using CarShare.DAL.Entities;
    using CarShare.View;
    using CarShare.ViewModel.BaseClass;
    using Model;
    using System;
    using System.Collections.ObjectModel;

    class VehiclesViewModel : ViewModelBase
    {
        #region Private Components
        private DBConnection dBConnection;
        private Model model = null;
        public ObservableCollection<string> VehicleList = new ObservableCollection<string>();

        #endregion

        #region Constructors
        public VehiclesViewModel(Model model)
        {
            this.model = model;
            LoadVehiclesToList();
        }

        public void LoadVehiclesToList()
        {
            Vehicles = model.Vehicles;
            VehicleList = new ObservableCollection<string>();
            foreach (var vehicle in Vehicles)
            {
                if (vehicle.IsForSale == true)
                {
                VehicleList.Add($"{vehicle.Maker} {vehicle.Model} {vehicle.Version} {vehicle.Engine} {vehicle.Power} {vehicle.ModelYear} {vehicle.HighestBid}");
                }
            }
        }

        #endregion
        #region ViewChanger
        public VehiclesViewModel()
        {
            dBConnection = new DBConnection();
        }
        #endregion

        #region Properties
        public ObservableCollection<Vehicle> Vehicles { get; set; }
        public ObservableCollection<string> vehicleList
        {
            get => VehicleList;
            set
            {
                VehicleList = value;
                onPropertyChanged(nameof(vehicleList));
            }
        }
        #endregion

    }
}