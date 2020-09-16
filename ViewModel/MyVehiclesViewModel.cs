namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using BaseClass;
    using CarShare.DAL.Entities;
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CarShare.DAL.Repos;
    using CarShare.View;

    class MyVehiclesViewModel : ViewModelBase
    {
        #region Private Components
        private DBConnection dBConnection;
        string maker, carModel, version, engine;
        int power, modelyear;
        sbyte vehicleID;
        private Model model = null;
        private int selectedVehicle;
        private ObservableCollection<Vehicle> vehicles { get; set; } = new ObservableCollection<Vehicle>();

        public ObservableCollection<string> vehiclesList = new ObservableCollection<string>();
        private UserInfo userInfo = UserInfo.Instance;


        #endregion

        #region Constructors
        public MyVehiclesViewModel(Model model)
        {
            this.model = model;
            //vehicles = model.UserVehicles;
            var currentUser = userInfo.currentUser;
            var userVehicles = VehicleRepo.GetAllUserVehicles(currentUser);
            foreach (var uV in userVehicles)
            {
                vehicles.Add(uV);
            }

        }

        public void VehicleToList()
        {
            Vehicles = model.Vehicles;
            vehiclesList = new ObservableCollection<string>();
            foreach(var v in Vehicles)
            {
                vehiclesList.Add($"{v.Maker} {v.Model} {v.Version} {v.Engine} {v.Power} {v.ModelYear}");
            }
        }
        #endregion

        #region ViewChanger
        public MyVehiclesViewModel()
        {
            dBConnection = new DBConnection();
        }
        #endregion

        #region Properties

        public ObservableCollection<Vehicle> Vehicles { get; set; }

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
        public string Maker
        {
            get => maker;
            set
            {
                maker = value;
                onPropertyChanged(nameof(Maker));
            }
        }
        public string Model
        {
            get => carModel;
            set
            {
                carModel = value;
                onPropertyChanged(nameof(Model));
            }
        }

        public string Version
        {
            get => version;
            set
            {
                version = value;
                onPropertyChanged(nameof(Version));
            }
        }

        public string Engine
        {
            get => engine;
            set
            {
                engine = value;
                onPropertyChanged(nameof(Engine));
            }
        }
        public int Power
        {
            get => power;
            set
            {
                power = value;
                onPropertyChanged(nameof(Power));
            }
        }
        public int ModelYear
        {
            get => modelyear;
            set
            {
                modelyear = value;
                onPropertyChanged(nameof(ModelYear));
            }
        }

        #endregion


        #region Methods

        private void GetVehicle(int vehicleID)
        {
            VehicleID = (sbyte)vehicles[vehicleID].VehicleID;
            Maker = vehicles[vehicleID].Maker;
            Model = vehicles[vehicleID].Model;
            Version = vehicles[vehicleID].Version;
            Engine = vehicles[vehicleID].Engine;
            Power = vehicles[vehicleID].Power;
            ModelYear = vehicles[vehicleID].ModelYear;

        }

        private void ClearAll()
        {
            Maker = "";
        }
        private ICommand loadVehicle = null;
        public ICommand LoadVehicle
        {
            get
            {
                if (loadVehicle == null)
                {
                    loadVehicle = new RelayCommand(arg =>
                    {
                        if (SelectedVehicle != -1)
                        {
                            GetVehicle(SelectedVehicle);
                        }
                        else
                        {
                            ClearAll();
                        }

                    },
                      arg => true);
                }
                return loadVehicle;
            }
        }

        private ICommand addVehicle = null;
        public ICommand AddVehicle
        {
            get
            {
                if (addVehicle != null)
                    return addVehicle;
                addVehicle = new RelayCommand(
                    arg =>
                    {
                        var vehicle = new Vehicle(Maker,Model,Version,Engine,Power,ModelYear,userInfo.currentUser);
                        if (!model.AddVehicle(vehicle))
                            return;
                        VehicleToList();
                        ClearAll();
                        onPropertyChanged(nameof(vehiclesList));
                        onPropertyChanged(nameof(maker));
                        onPropertyChanged(nameof(carModel));
                        onPropertyChanged(nameof(version));
                        onPropertyChanged(nameof(engine));
                        onPropertyChanged(nameof(power));
                        onPropertyChanged(nameof(modelyear));
                        onPropertyChanged(nameof(userInfo.currentUser));
                        System.Windows.MessageBox.Show($"{Maker} {Model} has been added to database!");
                    },
                    arg => (Model != "") && (Maker != "") && (Version != "") && (Engine != "") && (Power != 0) && (ModelYear != 2020)
                    );
                return addVehicle;
            }
        }

        private ICommand editVehicle = null;
        public ICommand EditVehicle
        {
            get
            {
                if (editVehicle != null) return editVehicle;
                editVehicle = new RelayCommand(
                    arg =>
                    {
                        var vehicle = new Vehicle(Maker, Model, Version, Engine, Power, ModelYear);
                        var idVhcl = SelectedVehicle;
                        sbyte id = VehicleID;

                        if (!model.EditVehicleInDB(vehicle, id)) return;
                        System.Windows.MessageBox.Show($"{Maker} {Model} has been edited in database!");
                        VehicleToList();
                        ClearAll();
                        onPropertyChanged(nameof(vehiclesList));
                        onPropertyChanged(nameof(maker));
                        onPropertyChanged(nameof(carModel));
                        onPropertyChanged(nameof(version));
                        onPropertyChanged(nameof(engine));
                        onPropertyChanged(nameof(power));
                        onPropertyChanged(nameof(modelyear));
                    },
                    arg => (Model != "") && (Maker != "") && (Version != "") && (Engine != "") && (Power != 0) && (ModelYear != 2020)
                );

                return editVehicle;
            }
        }

        private ICommand deleteVehicle = null;
        public ICommand DeleteVehicle
        {
            get
            {
                if (deleteVehicle != null) return deleteVehicle;
                deleteVehicle = new RelayCommand(
                    arg =>
                    {
                        var idToRemove = SelectedVehicle;
                        sbyte id = VehicleID;
                        model.RemoveVehicleFromDb(id);
                        System.Windows.MessageBox.Show($"{Maker} {Model} has been removed!");
                        VehicleToList();
                        onPropertyChanged(nameof(vehiclesList));
                    },
                    arg => SelectedVehicle != -1
                );

                return deleteVehicle;
            }
        }
        #endregion
    }
}
