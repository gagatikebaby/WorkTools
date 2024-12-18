using System.Windows;
using UIDesign.VIewModel;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace UIDesign.View
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : INavigableView<SettingVM>
    {
        public SettingVM ViewModel { get; }
        //public SettingPage()
        //{
        //    InitializeComponent();
        //}

        public SettingPage()
        {
            ViewModel = new SettingVM(navigationService: null);
            DataContext = this;

            InitializeComponent();
        }

        public SettingPage(SettingVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;

            InitializeComponent();
        }

    }
}
