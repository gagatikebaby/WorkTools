using Wpf.Ui.Controls;

namespace WorkToolsSln.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class DBListPage : INavigableView<VIewModel.DBListVM>
    {
        public VIewModel.DBListVM ViewModel { get; }

        private void DBListPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.RefreshList();
        }

        public DBListPage(VIewModel.DBListVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
            this.Loaded += DBListPage_Loaded;
        }
    }
}
