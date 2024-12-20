using WorkToolsSln.VIewModel;
using Wpf.Ui.Controls;


namespace WorkToolsSln.View.SubWindows
{
    /// <summary>
    /// Interaction logic for FilePathSettingWin.xaml
    /// </summary>
    public partial class FilePathSettingWin 
    {
        private SubWindowsVM _viewModel;
        public FilePathSettingWin()
        {
            InitializeComponent();
            _viewModel = new SubWindowsVM();

            this.DataContext = _viewModel;
        }
    }
}
