using Wpf.Ui.Controls;

namespace WorkToolsSln.View
{
    /// <summary>
    /// Interaction logic for UserControl1.xaml
    /// </summary>
    public partial class OperationLogPage : INavigableView<VIewModel.OperationLogVM>
    {
        public VIewModel.OperationLogVM ViewModel { get; }

        private void DBListPage_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            ViewModel.RefreshList();
        }

        public OperationLogPage(VIewModel.OperationLogVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;
            InitializeComponent();
            this.Loaded += DBListPage_Loaded;
        }
    }
}
