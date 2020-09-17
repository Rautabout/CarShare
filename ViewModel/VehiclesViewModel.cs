namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using BaseClass;
    using CarShare.DAL.Entities;
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CarShare.DAL.Repos;

    class VehiclesViewModel : ViewModelBase
    {
        #region Private Components
        private DBConnection dBConnection;
        private Model model = null;
        private int selectedVehicle,highestBid,userBid;
        private sbyte vehicleID;
        private ObservableCollection<Vehicle> vehicles { get; set; } = new ObservableCollection<Vehicle>();
        private UserInfo userInfo = UserInfo.Instance;


        #endregion

        #region Constructors
        public VehiclesViewModel(Model model)
        {
            this.model = model;
            var currentUser = userInfo.currentUser;
            var userVehicles = VehicleRepo.GetAllButUserVehicles(currentUser);
            foreach (var uV in userVehicles)
            {
                vehicles.Add(uV);
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
        public sbyte VehicleID
        {
            get => vehicleID;
            set
            {
                vehicleID = value;
                onPropertyChanged(nameof(VehicleID));
            }
        }
        public sbyte UserID
        {
            get => userInfo.currentUser;
            set
            {
                userInfo.currentUser = value;
                onPropertyChanged(nameof(UserID));
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
        public int UserBid
        {
            get => userBid;
            set
            {
                userBid = value;
                onPropertyChanged(nameof(UserBid));
            }
        }

        #endregion

        #region Methods

        private void GetBid(int vehicleID)
        {
            VehicleID = (sbyte)vehicles[vehicleID].VehicleID;
            HighestBid = vehicles[vehicleID].HighestBid;
        }

        private void ClearAll()
        {
            UserBid = 1;
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

        private ICommand addBid = null;
        public ICommand AddBid
        {
            get
            {
                if (addBid != null)
                    return addBid;
                addBid = new RelayCommand(
                    arg =>
                    {
                        if (UserBid == HighestBid)
                        {
                            System.Windows.MessageBox.Show($"Bid to low!");
                            return;
                        }
                        var bid = new Bid(UserID,VehicleID,UserBid);
                        if (!model.AddBid(bid))
                            return;
                        ClearAll();
                        onPropertyChanged(nameof(userInfo.currentUser));
                        onPropertyChanged(nameof(vehicleID));
                        onPropertyChanged(nameof(userBid));
                        System.Windows.MessageBox.Show($"Bid has been added to database!");
                    },
                    arg => ( UserBid>=HighestBid)
                    );
                return addBid;
            }
        }
        #endregion

    }
}