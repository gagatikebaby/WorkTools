//using Level1.Squirrel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Windows.Themes;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Windows;
using WorkToolsSln.Helper;
using WorkToolsSln.Service;
using WorkToolsSln.utils;
using Wpf.Ui;

namespace WorkToolsSln
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            _host.Start();
            // 初始化数据库
            SQLiteHelper.InitializeDatabase();
        }

        private static readonly IHost _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(c =>
            {
                c.SetBasePath(AppContext.BaseDirectory);
            })
            .ConfigureServices(
            (_, services) =>
            {
                //services.AddHostedService<ApplicationHostService>();
                services.AddSingleton<INavigationService, NavigationService>();
                services.AddSingleton<WindowsSrc>();
            }
            )
            .Build();

        /// <summary>
        /// Gets registered service.
        /// </summary>
        /// <typeparam name="T">Type of the service to get.</typeparam>
        /// <returns>Instance of the service or <see langword="null"/>.</returns>
        public static T GetRequiredService<T>()
            where T : class
        {
            return _host.Services.GetRequiredService<T>();
        }

        /// <summary>
        /// Occurs when the application is closing.
        /// </summary>
        private void OnExit(object sender, ExitEventArgs e)
        {
            _host.StopAsync().Wait();

            _host.Dispose();
        }
    }

}
