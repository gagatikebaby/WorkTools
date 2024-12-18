//using Level1.Squirrel;
using Microsoft.Windows.Themes;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Windows;
using UIDesign.Helper;
using UIDesign.utils;

namespace UIDesign
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 初始化数据库
            SQLiteHelper.InitializeDatabase();

         
        }
    }

}
