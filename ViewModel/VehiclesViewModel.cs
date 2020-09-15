namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using BaseClass;
    using CarShare.DAL.Entities;
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows.Input;

    class VehiclesViewModel : ViewModelBase
    {
        #region Private Components
        private DBConnection dBConnection;
        private Model model = null;
        private int selectedVehicle,highestBid;
        private ObservableCollection<Vehicle> vehicles { get; set; }

        #endregion

        #region Constructors
        public VehiclesViewModel(Model model)
        {
            this.model = model;
            vehicles = model.Vehicles;

        }


        #endregion
        #region ViewChanger
        public VehiclesViewModel()
        {
            dBConnection = new DBConnection();
        }
        #endregion

        #region Properties
        
        public ObservableCollection<Vehicle> VehiclesList
        {
            get { return vehicles; }
            set
            {
                vehicles = value;
                onPropertyChanged(nameof(VehiclesList));
            }
        }

        public int SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                selectedVehicle = value;
                onPropertyChanged(nameof(SelectedVehicle));
            }
        }
        public int HighestBid
        {
            get => highestBid;
            set
            {
                highestBid = value;
                onPropertyChanged(nameof(HighestBid));
            }
        }
        #endregion

        #region Methods

        private void GetBid(int vehicleID)
        {
            HighestBid = vehicles[vehicleID].HighestBid;
        }

        private void ClearAll()
        {
            HighestBid = 1;

        }
        private ICommand loadBid = null;
        public ICommand LoadBid
        {
            get
            {
                if (loadBid == null)
                {
                    loadBid = new RelayCommand(arg =>
                      {
                          if (SelectedVehicle != -1)
                          {
                              GetBid(SelectedVehicle);
                          }
                          else
                          {
                              ClearAll();
                          }

                      },
                      arg => true);
                }
                return loadBid;
            }
        }

        
        #endregion

    }
}