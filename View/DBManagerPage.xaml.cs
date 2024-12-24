using WorkToolsSln.VIewModel;
using Wpf.Ui;
using Wpf.Ui.Controls;


namespace WorkToolsSln.View
{
    /// <summary>
    /// Interaction logic for UIDataBaseManager.xaml
    /// </summary>
    public partial class DBManagerPage : INavigableView<VIewModel.DBManagerVM>
    {
        public VIewModel.DBManagerVM ViewModel { get; }

        public DBManagerPage(DBManagerVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = ViewModel;

            InitializeComponent();
        }
    }
}
