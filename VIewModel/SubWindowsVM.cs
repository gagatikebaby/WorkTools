using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
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
        private string _dependencyPath = string.Empty;

        [ObservableProperty]
        private string _unpackagePath = string.Empty;


        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "PathConfig.json");
        private PathConfigInfo _PathList = new PathConfigInfo();
        public SubWindowsVM()
        {
            _PathList = FileOperation.ReadConfig<PathConfigInfo>(ConfigPath);
            DependencyPath = _PathList.DependencyPath;
            UnpackagePath = _PathList.UnpackagePath;
        }

        [RelayCommand]
        public void OnSelectDependcyPath()
        {
            string ret = SelectFolder();
            if (string.IsNullOrEmpty(ret))
            {
                return;
            }
            DependencyPath = ret;
            _PathList.DependencyPath = DependencyPath;
            FileOperation.WriteConfig<PathConfigInfo>(ConfigPath, _PathList);
        }

        public void OnSelectUnpackagePath()
        {
            string selsctpath = SelectFolder();

            if (string.IsNullOrEmpty(selsctpath))
            {
                return;
            }
            UnpackagePath = selsctpath;
            _PathList.UnpackagePath = UnpackagePath;
            FileOperation.WriteConfig<PathConfigInfo>(ConfigPath, _PathList);
        }

        private string SelectFolder()
        {
            OpenFolderDialog openFolderDialog = new()
            {
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            if (openFolderDialog.ShowDialog() != true)
            {
                return null;
            }

            if (openFolderDialog.FolderNames.Length == 0)
            {
                return null;
            }

            // 返回选中的路径
            return string.Join("\n", openFolderDialog.FolderNames);
        }
    }
}
