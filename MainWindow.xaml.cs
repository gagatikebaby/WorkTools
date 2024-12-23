using Microsoft.Windows.Themes;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WorkToolsSln.Helper;
using WorkToolsSln.Model;
using WorkToolsSln.Service.Contracts;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;
namespace WorkToolsSln
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IWindow
    {
        private GlobalConfig _globalConfig = new GlobalConfig();
        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "GlobalConfig.json");
        public MainWindow(INavigationService navigationService,
        IServiceProvider serviceProvider,
        ISnackbarService snackbarService
        )
        {
            InitializeComponent(); // 必须调用此方法来初始化XAML定义的UI组件
            LoadIcon();
            InitTheme();
            snackbarService.SetSnackbarPresenter(SnackbarPresenter);
            navigationService.SetNavigationControl(NavigationView);
            NavigationView.SetServiceProvider(serviceProvider);
        }

        private void FluentWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void LoadIcon()
        {
            string iconPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "wpfui.png");
            TitleBar.Icon = new ImageIcon
            {
                Source = new BitmapImage(new Uri(iconPath, UriKind.Absolute))
            };
        }

        private void InitTheme()
        {
            _globalConfig = FileOperation.ReadConfig<GlobalConfig>(ConfigPath);

            // 使用 Enum.TryParse 来安全地转换字符串到枚举
            if (Enum.TryParse(_globalConfig.Theme.ToString(), out ApplicationTheme theme))
            {
                ApplicationThemeManager.Apply(theme);
            }
            else
            {
                // 如果解析失败，可以应用默认主题或处理错误
                ApplicationThemeManager.Apply(ApplicationTheme.Light); // 举例：默认应用 Light 主题
            }
        }

    }
}