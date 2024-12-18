using WorkToolsSln.VIewModel;
using Wpf.Ui.Controls;

namespace WorkToolsSln.View
{
    /// <summary>
    /// Interaction logic for SvnOperationPage.xaml
    /// </summary>
    public partial class SvnOperationPage : INavigableView<DailyOperationVM>
    {
        public DailyOperationVM ViewModel { get;  }
        public SvnOperationPage()
        {
            ViewModel = new DailyOperationVM(navigationService: null);
            InitializeComponent();
            this.DataContext = ViewModel;
        }

        public SvnOperationPage(DailyOperationVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }
    }
}
