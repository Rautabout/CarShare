

namespace CarShare.ViewModel
{
    using CarShare.DAL;
    using BaseClass;
    using CarShare.DAL.Entities;
    using Model;
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using CarShare.DAL.Repos;
    using System.Windows.Documents;
    using System.Windows.Data;

    class MyBidsViewModel : ViewModelBase
    {
        #region Private Components
        private DBConnection dBConnection;
        private Model model = null;
        private int selectedBid;
        private ObservableCollection<Bid> bids { get; set; } = new ObservableCollection<Bid>();
        private ObservableCollection<Vehicle> vehicles { get; set; } = new ObservableCollection<Vehicle>();


        #endregion
        #region Constructors
        public MyBidsViewModel(Model model)
        {
            //this.model = model;
            //vehicles = model.UserVehicles;
            var userInfo = UserInfo.Instance;
            var currentUser = userInfo.currentUser;
            var userBids = BidRepo.GetAllUserBids(currentUser);
            foreach (var uB in userBids)
            {
                var vehiclesID = VehicleRepo.GetAllVehiclesByID((sbyte)uB.VehicleID);
                bids.Add(uB);
                foreach(var v in vehiclesID)
                {
                    vehicles.Add(v);
                    
                }
            }

        }
        #endregion

        #region ViewChanger
        public MyBidsViewModel()
        {
            dBConnection = new DBConnection();

            
        }
        #endregion

        #region Properties

        public ObservableCollection<Bid> BidsList
        {
            get { return bids; }
            set
            {
                bids = value;
                onPropertyChanged(nameof(BidsList));
            }
        }
        public ObservableCollection<Vehicle> VehiclesList
        {
            get{ return vehicles; }
            set
            {
                vehicles = value;
                onPropertyChanged(nameof(VehiclesList));
            }
        }

        public int SelectedBid
        {
            get => selectedBid;
            set
            {
                selectedBid = value;
                onPropertyChanged(nameof(SelectedBid));
            }
        }

        #endregion

    }
}
