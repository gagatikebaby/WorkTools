using Microsoft.Windows.Themes;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WorkToolsSln.Service.Contracts;
using WorkToolsSln.utils;
using WorkToolsSln.View;
using Wpf.Ui;
using Wpf.Ui.Controls;
namespace WorkToolsSln
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IWindow
    {
        public MainWindow(INavigationService navigationService,
        IServiceProvider serviceProvider,
        ISnackbarService snackbarService
        )
        {
            InitializeComponent(); // 必须调用此方法来初始化XAML定义的UI组件
            LoadICON();
            //Loaded += (_, _) => NavigationView.Navigate(typeof(DailyOperationPage));
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            navigationService.SetNavigationControl(NavigationView);
            //contentDialogService.SetDialogHost(RootContentDialog);

            NavigationView.SetServiceProvider(serviceProvider);
        }

        private void FluentWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LoadICON()
        {
            string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "wpfui.png");
            TitleBar.Icon = new ImageIcon
            {
                Source = new BitmapImage(new Uri(iconPath, UriKind.Absolute))
            };
        }
    }
}