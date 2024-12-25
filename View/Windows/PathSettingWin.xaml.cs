using WorkToolsSln.VIewModel;
using Wpf.Ui.Controls;


namespace WorkToolsSln.View.Windows
{
    /// <summary>
    /// Interaction logic for PathSettingWin.xaml
    /// </summary>
    public partial class PathSettingWin 
    {
        private SubWindowsVM _viewModel;
        public PathSettingWin()
        {
            InitializeComponent();
            _viewModel = new SubWindowsVM();

            this.DataContext = _viewModel;
        }
    }
}
