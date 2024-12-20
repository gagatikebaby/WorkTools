using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Media;
using WorkToolsSln.Model;
using WorkToolsSln.Service;
using WorkToolsSln.View.SubWindows;
using Wpf.Ui;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace WorkToolsSln.VIewModel
{
    public sealed partial class SettingVM(WindowsSrc _windowsSrc) : ViewModel
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private ApplicationTheme _currentApplicationTheme = ApplicationTheme.Dark;

        [ObservableProperty]
        private IEnumerable<WindowCard> _windowCards = new WindowCard[]
        {
        new("路径配置", "设置文件夹路径及授权码", SymbolRegular.DocumentSettings16, "PathSetting"),
        };

        public override void OnNavigatedTo()
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
            }
        }

        private void InitializeViewModel()
        {
            CurrentApplicationTheme = ApplicationThemeManager.GetAppTheme();

            ApplicationThemeManager.Changed += OnThemeChanged;
            _isInitialized = true;
        }

        private void OnThemeChanged(ApplicationTheme currentApplicationTheme, Color systemAccent)
        {
            // Update the theme if it has been changed elsewhere than in the settings.
            if (CurrentApplicationTheme != currentApplicationTheme)
            {
                CurrentApplicationTheme = currentApplicationTheme;
            }
        }

        partial void OnCurrentApplicationThemeChanged(ApplicationTheme oldValue, ApplicationTheme newValue)
        {
            ApplicationThemeManager.Apply(newValue);
        }

        [RelayCommand]
        public void OnOpenWindow(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return;
            }

            FilePathSettingWin ui = new FilePathSettingWin();
            //_windowsSrc = new WindowsSrc(serviceProvider : null);
            switch (value)
            {
                case "PathSetting":
                    //_windowsSrc.Show<FilePathSettingWin>();
                    ui.Show();
                    break;
            }
        }
    }
}
