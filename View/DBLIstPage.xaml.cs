using Wpf.Ui.Controls;

namespace UIDesign.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DBListPage : INavigableView<VIewModel.DBListVM>
    {
        public VIewModel.DBListVM ViewModel { get; }

        public DBListPage()
        {
            ViewModel = new VIewModel.DBListVM(navigationService: null);
            InitializeComponent();
            DataContext = ViewModel;
            this.Loaded += DBListPage_Loaded;
        }

        private void DBListPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.RefreshList();
        }

        public DBListPage(VIewModel.DBListVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
