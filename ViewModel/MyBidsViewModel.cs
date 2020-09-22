

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
        private int currentBid, highestBid, selectedBid;
        sbyte bidID,userID,vehicleID;
        private ObservableCollection<VehicleBid> vehicleBids { get; set; } = new ObservableCollection<VehicleBid>();
        private ObservableCollection<string> bidsList = new ObservableCollection<string>();



        #endregion
        #region Constructors
        public MyBidsViewModel(Model model)
        {
            this.model = model;
            var userInfo = UserInfo.Instance;
            var currentUser = userInfo.currentUser;
            var userBids = VehicleBidRepo.GetAllUserVehicleBid(currentUser);
            foreach (var uB in userBids)
            {
                vehicleBids.Add(uB);
            }

        }
        public void BidsToList()
        {
            Bids = model.Bids;
            bidsList = new ObservableCollection<string>();
            foreach (var b in Bids)
            {
                bidsList.Add($"{b.BidID} {b.UserID} {b.VehicleID} {b.CurrentBid}");
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
        public ObservableCollection<Bid> Bids { get; set; }


        public ObservableCollection<VehicleBid> BidsList
        {
            get { return vehicleBids; }
            set
            {
                vehicleBids = value;
                onPropertyChanged(nameof(BidsList));
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
        public int CurrentBid
        {
            get => currentBid;
            set
            {
                currentBid = value;
                onPropertyChanged(nameof(CurrentBid));
            }
        }
        public sbyte BidID
        {
            get => bidID;
            set
            {
                bidID = value;
                onPropertyChanged(nameof(BidID));
            }
        }
        public sbyte UserID
        {
            get => userID;
            set
            {
                userID = value;
                onPropertyChanged(nameof(UserID));
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

        private void GetBid(int bidID)
        {
            BidID = (sbyte)vehicleBids[bidID].BidID;
            VehicleID = (sbyte)vehicleBids[bidID].VehicleID;
            HighestBid = vehicleBids[bidID].HighestBid;
        }

        private void ClearAll()
        {
            BidID = 0;
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
                        if (SelectedBid != -1)
                        {
                            GetBid(SelectedBid);
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

        private ICommand deleteBid = null;
        public ICommand DeleteBid
        {
            get
            {
                if (deleteBid != null) return deleteBid;
                deleteBid = new RelayCommand(
                    arg =>
                    {
                        var idToRemove = SelectedBid;
                        sbyte id = BidID;
                        model.RemoveBidFromDb(id);
                        System.Windows.MessageBox.Show($"Bid has been removed!");
                        BidsToList();
                        onPropertyChanged(nameof(bidsList));
                    },
                    arg => SelectedBid != -1
                );

                return deleteBid;
            }
        }

        private ICommand editBid = null;
        public ICommand EditBid
        {
            get
            {
                if (editBid != null) return editBid;
                editBid = new RelayCommand(
                    arg =>
                    {
                        if (CurrentBid == HighestBid)
                        {
                            System.Windows.MessageBox.Show($"Bid to low!");
                            return;
                        }
                        var bid = new Bid(UserID,VehicleID,CurrentBid);
                        var idBid = SelectedBid;
                        sbyte id = BidID;

                        if (!model.EditBidInDB(bid, id)) return;
                        System.Windows.MessageBox.Show($"Bid has been edited in database!");
                        BidsToList();
                        ClearAll();
                        onPropertyChanged(nameof(userID));
                        onPropertyChanged(nameof(vehicleID));
                        onPropertyChanged(nameof(currentBid));
                       
                    },
                    arg => (CurrentBid!=0)
                );

                return editBid;
            }
        }
        #endregion
    }
}
