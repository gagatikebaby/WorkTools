using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkToolsSln.Helper;
using WorkToolsSln.Model;

namespace WorkToolsSln.VIewModel
{
    public partial class SubWindowsVM : ViewModel
    {
        [ObservableProperty]
        private string dependencyPath = string.Empty;
        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "PathConfig.json");
        private PathConfigInfo _PathList = new PathConfigInfo();
        public SubWindowsVM()
        {
            _PathList =  FileOperation.ReadConfig<PathConfigInfo>(ConfigPath);
            dependencyPath = _PathList.DependencyPath;
        }
    }
}
