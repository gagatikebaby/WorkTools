using Microsoft.Windows.Themes;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using UIDesign.utils;
using UIDesign.View;
using Wpf.Ui.Controls;
namespace UIDesign
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : FluentWindow
    {
        public MainWindow()
        {
            InitializeComponent(); // 必须调用此方法来初始化XAML定义的UI组件
            LoadICON();
            Loaded += (_, _) => RootNavigation.Navigate(typeof(DBManagerPage));
           
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