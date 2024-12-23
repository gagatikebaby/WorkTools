//using Level1.Squirrel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Windows.Themes;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Windows;
using System.Windows.Threading;
using WorkToolsSln.Helper;
using WorkToolsSln.Service;
using WorkToolsSln.Service.Contracts;
using WorkToolsSln.utils;
using WorkToolsSln.View;
using WorkToolsSln.VIewModel;
using Wpf.Ui;

namespace WorkToolsSln
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void OnStartup(object sender, StartupEventArgs e)
        {
            SQLiteHelper.InitializeDatabase();

            _host.Start();

        }
        //protected override void OnStartup(StartupEventArgs e)
        //{
        //    SQLiteHelper.InitializeDatabase();


        //    _host.Start();
        //    // 初始化数据库
        //    //base.OnStartup(e);
        //}
        private static readonly IHost _host = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration(c =>
            {
                _ = c.SetBasePath(AppContext.BaseDirectory);
            })
            .ConfigureServices(
            (_1, services) =>
            {
                _ = services.AddHostedService<ApplicationHostService>();

                _ = services.AddSingleton<IWindow, MainWindow>();
                _ = services.AddSingleton<INavigationService, NavigationService>();
                _ = services.AddSingleton<ISnackbarService, SnackbarService>();
                _ = services.AddSingleton<IContentDialogService, ContentDialogService>();
                
                _ = services.AddSingleton<WindowsSrc>();

                // Top-level pages
                _ = services.AddSingleton<DBManagerPage>();
                _ = services.AddSingleton<DBManagerVM>();
                _ = services.AddSingleton<DBListPage>();
                _ = services.AddSingleton<DBListVM>();
                _ = services.AddSingleton<DailyOperationPage>();
                _ = services.AddSingleton<DailyOperationVM>();
                _ = services.AddSingleton<SettingPage>();
                _ = services.AddSingleton<SettingVM>();
                //_ = services.AddSingleton<WindowsSrc, SettingVM>();
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

        /// <summary>
        /// Occurs when an exception is thrown by an application but not handled.
        /// </summary>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        }


    }

}
