using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Wpf.Ui;
using Wpf.Ui.Appearance;

namespace UIDesign.VIewModel
{
    public sealed partial class SettingVM(INavigationService navigationService) : ViewModel
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private ApplicationTheme _currentApplicationTheme = ApplicationTheme.Dark;

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
    }
}
