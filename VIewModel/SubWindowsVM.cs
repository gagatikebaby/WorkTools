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
        private string dependencyPath = string.Empty;
        [ObservableProperty]
        private string _openedFolderPath = string.Empty;


        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "PathConfig.json");
        private PathConfigInfo _PathList = new PathConfigInfo();
        public SubWindowsVM()
        {
            _PathList =  FileOperation.ReadConfig<PathConfigInfo>(ConfigPath);
            OpenedFolderPath = _PathList.DependencyPath;
        }

        [RelayCommand]
        public void OnSelectDependcyPath()
        {

            OpenFolderDialog openFolderDialog = new()
            {
                Multiselect = true,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

                    if (openFolderDialog.ShowDialog() != true)
            {
                return;
            }

            if (openFolderDialog.FolderNames.Length == 0)
            {
                return;
            }

            OpenedFolderPath = string.Join("\n", openFolderDialog.FolderNames);
        }

    }
}
