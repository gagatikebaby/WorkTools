using System.Windows;
using WorkToolsSln.Effects;
using WorkToolsSln.VIewModel;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace WorkToolsSln.View
{
    /// <summary>
    /// Interaction logic for SettingPage.xaml
    /// </summary>
    public partial class SettingPage : INavigableView<SettingVM>
    {
        public SettingVM ViewModel { get; }
        private SnowflakeEffect? _snowflake;
        public SettingPage(SettingVM viewModel)
        {
            ViewModel = viewModel;
            DataContext = this;
            InitializeComponent();
            this.Loaded += HandleLoaded;
            this.Unloaded += HandleUnloaded;
        }

        private void HandleUnloaded(object sender, RoutedEventArgs e)
        {
            _snowflake?.Stop();
            _snowflake = null;
        }

        private void HandleLoaded(object sender, RoutedEventArgs e)
        {
            _snowflake ??= new(MainCanvas);
            _snowflake.Start();
        }
    }
}
