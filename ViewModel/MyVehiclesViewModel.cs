namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using BaseClass;
    using CarShare.DAL.Entities;
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CarShare.DAL.Repos;

    class MyVehiclesViewModel : ViewModelBase
    {
        #region Private Components
        private DBConnection dBConnection;
        private Model model = null;
        private int selectedVehicle;
        private ObservableCollection<Vehicle> vehicles { get; set; } = new ObservableCollection<Vehicle>();

        #endregion

        #region Constructors
        public MyVehiclesViewModel(Model model)
        {
            //this.model = model;
            //vehicles = model.UserVehicles;
            var userInfo = UserInfo.Instance;
            var currentUser = userInfo.currentUser;
            var userVehicles = VehicleRepo.GetAllUserVehicles(currentUser);
            foreach (var uV in userVehicles)
            {
                vehicles.Add(uV);
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

        #endregion
    }
}
