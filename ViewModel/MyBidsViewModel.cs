

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
        private int selectedBid, highestBid;
        sbyte bidID;
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
        public sbyte BidID
        {
            get => bidID;
            set
            {
                bidID = value;
                onPropertyChanged(nameof(BidID));
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
        #endregion
    }
}
