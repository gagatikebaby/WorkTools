using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WorkToolsSln.Helper;
using WorkToolsSln.Model;
using WorkToolsSln.utils;
using Wpf.Ui;

namespace WorkToolsSln.VIewModel
{
    public partial class DataDisplayVM : ViewModel
    {
        private readonly INavigationService _navigationService;
        private AboutConfig _aboutConfig { get; set; }
        private AboutConfig _aboutConfigD { get; set; }
        private ProductInfoConfig _productInfoConfig { get; set; }
        private ProductInfoConfig _productInfoConfigD { get; set; }

        private PathConfigInfo _pathConfigInfo { get; set; }
        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "PathConfig.json");

        public DataDisplayVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            ReadConfig();
            ReadConfigD();
            InitCommands();
        }

        public override void OnNavigatedTo()
        {
            ReadConfig();
            ReadConfigD();
        }

        private void ReadConfig()
        {
            _pathConfigInfo = FileOperation.ReadConfig<PathConfigInfo>(ConfigPath);
            string path = Path.Combine(_pathConfigInfo.UnpackagePath, "Configuration", "AboutConfig.json");
            string path2 = Path.Combine(_pathConfigInfo.UnpackagePath, "Configuration", "ProductInfoConfig.json");
            if (!File.Exists(path))
            {
                MessageBox.Show($"{path} 文件不存在，请检查！");
                return;
            }
            if(!File.Exists(path2))
            {
                MessageBox.Show($"{path2} 文件不存在，请检查！");
                return;
            }
            _aboutConfig = FileOperation.ReadConfig<AboutConfig>(path);
            _productInfoConfig = FileOperation.ReadConfig<ProductInfoConfig>(path2);
            Version = _aboutConfig.EquipmentIdentificationNumber;
            MachineType = _productInfoConfig.ProductType;
            if (_productInfoConfig.IsPets)
            {
                ProductType = "宠物";
            }
            else if (_productInfoConfig.IsWorkStation)
            {
                ProductType = "工作站";
            }
            else if (_productInfoConfig.IsMovable)
            {
                ProductType = "移动CT";
            }
            else
            {
                ProductType = "人医";
            }
        }

        private void ReadConfigD()
        {
            _pathConfigInfo = FileOperation.ReadConfig<PathConfigInfo>(ConfigPath);
            string path = Path.Combine(_pathConfigInfo.SoftWarePath, "Configuration", "AboutConfig.json");
            string path2 = Path.Combine(_pathConfigInfo.SoftWarePath, "Configuration", "ProductInfoConfig.json");
            if (!File.Exists(path))
            {
                MessageBox.Show($"{path} 文件不存在，请检查！");
                return;
            }
            if (!File.Exists(path2))
            {
                MessageBox.Show($"{path2} 文件不存在，请检查！");
                return;
            }
            _aboutConfig = FileOperation.ReadConfig<AboutConfig>(path);
            _productInfoConfig = FileOperation.ReadConfig<ProductInfoConfig>(path2);
            VersionD = _aboutConfig.EquipmentIdentificationNumber;
            MachineTypeD = _productInfoConfig.ProductType;
            if (_productInfoConfig.IsPets)
            {
                ProductTypeD = "宠物";
            }
            else if (_productInfoConfig.IsWorkStation)
            {
                ProductTypeD = "工作站";
            }
            else if (_productInfoConfig.IsMovable)
            {
                ProductTypeD = "移动CT";
            }
            else
            {
                ProductTypeD = "人医";
            }
        }

        private void InitCommands()
        {
            //SaveCommand = new RelayCommand(SaveCommandExecute);
        }

        public ICommand SaveCommand { get; protected set; }

        //private void SaveCommandExecute()
        //{
        //    DBOperation.Instance.AddRecord(Number, Price);
        //}

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

        [ObservableProperty]
        private string _version  = string.Empty;

        [ObservableProperty]
        private string _machineType = string.Empty;

        [ObservableProperty]
        private string _productType = string.Empty;


        [ObservableProperty]
        private string _versionD = string.Empty;

        [ObservableProperty]
        private string _machineTypeD = string.Empty;

        [ObservableProperty]
        private string _productTypeD = string.Empty;
    }
}
