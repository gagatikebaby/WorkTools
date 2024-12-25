using WorkToolsSln.VIewModel;
using Wpf.Ui;
using Wpf.Ui.Controls;


namespace WorkToolsSln.View
{
    /// <summary>
    /// Interaction logic for UIDataBaseManager.xaml
    /// </summary>
    public partial class DataDisplayPage : INavigableView<VIewModel.DataDisplayVM>
    {
        public VIewModel.DataDisplayVM ViewModel { get; }

        public DataDisplayPage(DataDisplayVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;

            InitializeComponent();
        }
    }
}
