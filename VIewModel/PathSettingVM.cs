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

        [ObservableProperty]
        private string _visualStdioPath = string.Empty;

        [ObservableProperty]
        private string _machine580 = string.Empty;

        [ObservableProperty]
        private string _machine750 = string.Empty;

        [ObservableProperty]
        private string _machine750P = string.Empty;

        [ObservableProperty]
        private string _machineWorkstationCode = string.Empty;

        [ObservableProperty]
        private string _machineWorkstationPassword = string.Empty;

        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "PathConfig.json");
        private PathConfigInfo _PathList = new PathConfigInfo();
        public SubWindowsVM()
        {
            _PathList = FileOperation.ReadConfig<PathConfigInfo>(ConfigPath);
            DependencyPath = _PathList.DependencyPath;
            UnpackagePath = _PathList.UnpackagePath;
            VisualStdioPath = _PathList.VisualStudioPath;
            _machine580 = _PathList.Q580Cfg1;
            _machine750 = _PathList.T750Cfg1;
            _machine750P = _PathList.Pet750Cfg1;
            _machineWorkstationCode = _PathList.workStation.Code;
            _machineWorkstationPassword = _PathList.workStation.PassWord;
        }


        public void SaveConfig()
        {
            _PathList.Q580Cfg1 = Machine580;
            _PathList.T750Cfg1 = Machine750;
            _PathList.Pet750Cfg1 = Machine750P;
            _PathList.workStation.Code = MachineWorkstationCode;
            _PathList.workStation.PassWord = MachineWorkstationPassword;
            FileOperation.WriteConfig<PathConfigInfo>(ConfigPath, _PathList);
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

        [RelayCommand]
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

        [RelayCommand]
        public void OnVisualStudioPath()
        {
            string ret = SelectFile();
            if (string.IsNullOrEmpty(ret))
            {
                return;
            }
            VisualStdioPath = ret;
            _PathList.VisualStudioPath = VisualStdioPath;
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

        private string SelectFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = false,  // 不允许多选
                Filter = "Executable Files (*.exe)|*.exe",  // 设置文件类型过滤器，只允许选择 .exe 文件
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) // 设置初始文件夹
            };

            if (openFileDialog.ShowDialog() != true)
            {
                return null;  // 如果用户没有选择文件，返回 null
            }

            // 返回选中的文件路径
            return openFileDialog.FileName;
        }

    }
}
