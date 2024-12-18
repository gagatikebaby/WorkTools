using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using UIDesign.Model;
using UIDesign.utils;
using Wpf.Ui;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UIDesign.VIewModel
{
    public class DBListVM : ObservableObject
    {
        private readonly INavigationService _navigationService;
        
        public DBListVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ReportInfo = new RecordListInfo();
            InitCommands();
        }

        public void RefreshList()
        {
            ReportInfo.InitData();
            TotalExpenses = ReportInfo.RecordInfoList.Where(a => a.Number >= 0).Sum(a => a.Number);
            TotalIncome = ReportInfo.RecordInfoList.Where(a => a.Price >= 0).Sum(a => a.Price);
        }

        private void InitCommands()
        {
            DeleteListItemCmd = new RelayCommand(DeleteListItemExecute);
        }


        public ICommand DeleteListItemCmd { get; protected set; }

        private void DeleteListItemExecute()
        {
            //DBOperation.Instance.AddRecord(Number, Price);
            DBOperation.Instance.DeleteRecord(ReportInfo.SelectedRecordInfo.DbInstanceUID);
            RefreshList();
        }


        #region Data
        private RecordListInfo reportInfo;
        public RecordListInfo ReportInfo
        {
            get => reportInfo;
            set => SetProperty(ref reportInfo, value);
              
        }

        private double totalexpenses;
        public double TotalExpenses
        {
            get => totalexpenses;
            set => SetProperty(ref totalexpenses, value);
        }

        private double totalincome;
        public double TotalIncome
        {
            get => totalincome;
            set => SetProperty(ref totalincome, value);
        }
        #endregion
    }
}
