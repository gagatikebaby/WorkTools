using Wpf.Ui;
using Wpf.Ui.Controls;


namespace UIDesign.View
{
    /// <summary>
    /// Interaction logic for UIDataBaseManager.xaml
    /// </summary>
    public partial class DBManagerPage : INavigableView<VIewModel.DBManagerVM>
    {
        public VIewModel.DBManagerVM ViewModel { get; }

        public DBManagerPage()
        {
            ViewModel = new VIewModel.DBManagerVM(navigationService: null);
            InitializeComponent();
            DataContext = ViewModel;
        }

        public DBManagerPage(VIewModel.DBManagerVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
