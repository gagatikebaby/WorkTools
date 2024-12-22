using WorkToolsSln.VIewModel;
using Wpf.Ui.Controls;

namespace WorkToolsSln.View
{
    /// <summary>
    /// Interaction logic for DailyOperationPage.xaml
    /// </summary>
    public partial class DailyOperationPage : INavigableView<DailyOperationVM>
    {
        public DailyOperationVM ViewModel { get;  }
        //public DailyOperationPage()
        //{
        //    ViewModel = new DailyOperationVM(navigationService: null);
        //    InitializeComponent();
        //    this.DataContext = ViewModel;
        //}

        public DailyOperationPage(DailyOperationVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;

            InitializeComponent();
        }
    }
}
