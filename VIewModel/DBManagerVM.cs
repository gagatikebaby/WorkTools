using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using UIDesign.utils;
using Wpf.Ui;

namespace UIDesign.VIewModel
{
    public partial class DBManagerVM : ObservableObject
    {
        private readonly INavigationService _navigationService;
        public DBManagerVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitCommands();
        }

        private void InitCommands()
        {
            SaveCommand = new RelayCommand(SaveCommandExecute);
        }



        public ICommand SaveCommand { get; protected set; }

        private void SaveCommandExecute()
        {
            DBOperation.Instance.AddRecord(Number, Price);
        }

        private int number;
        public int Number
        {
            get => number;
            set => SetProperty(ref number, value); // 使用 SetProperty 方法来更新属性并触发通知
        }

        private double price;
        public double Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }
    }
}
