﻿using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpSvn;
using System.IO;
using System.Windows;
using System.Windows.Input;
using WorkToolsSln.Helper;
using WorkToolsSln.Model;
using Wpf.Ui;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WorkToolsSln.utils;
using Microsoft.Extensions.DependencyModel;

namespace WorkToolsSln.VIewModel
{
    public sealed partial class DailyOperationVM : ViewModel
    {
        private readonly INavigationService _navigationService;
        private PathConfigInfo? pathConfigInfo { get; set; }
        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "PathConfig.json");
        private ObservableCollection<ObservableCollection<ButtonItem>> buttonList = new ObservableCollection<ObservableCollection<ButtonItem>>();
        public ObservableCollection<ObservableCollection<ButtonItem>> ButtonList
        {
            get { return buttonList; }
            set
            {
                buttonList = value;

                OnPropertyChanged("ButtonList");
            }
        }
        Wpf.Ui.Controls.MessageBox _messagebox { get; set; }

        #region Command
        public ICommand DependencyUpataCmd { get; private set; }
        public ICommand UnpackageUpataCmd { get; private set; }
        public ICommand UnpackageCleanUpCmd { get; private set; }
        public ICommand Denpenency2UnpackageCmd { get; private set; }
        public ICommand UnpackageCheckoutCmd { get; private set; }
        public ICommand DeleteDocumentCmd { get; private set; }
        public ICommand DependencyRevertCmd { get; private set; }
        #endregion

        public DailyOperationVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            LoadJsonData();
            InitUI();
            InitCommand();
        }

        

        public override void OnNavigatedTo()
        {
            LoadJsonData();
        }

        private void LoadJsonData()
        {
            pathConfigInfo = FileOperation.ReadConfig<PathConfigInfo>(ConfigPath);
        }

        private void InitUI()
        {
            ButtonList = new ObservableCollection<ObservableCollection<ButtonItem>>
            {
                new ObservableCollection<ButtonItem>
                {
                    new ButtonItem { Content = "启动软件", ClickCommand = new RelayCommand(() => StartExeExecute(Path.Combine(pathConfigInfo.UnpackagePath, "Vanguard.exe"))) },
                    new ButtonItem { Content = "KKKiller", ClickCommand = new RelayCommand(() => StartKillerExecute()) },
                    new ButtonItem { Content = "模拟器", ClickCommand = new RelayCommand(() => OpenSimulator()) },
                    //new ButtonItem { Content = "kill monitor", ClickCommand = new RelayCommand(() => killmonitorExecute()) },
                },
                new ObservableCollection<ButtonItem>
                {
                    new ButtonItem { Content = "打开Visual Studio", ClickCommand = new RelayCommand(() => StartExeExecute(pathConfigInfo.VisualStudioPath)) },
                    new ButtonItem { Content = "删除Unpackage", ClickCommand = new RelayCommand(() => DeleteDocumentExecute()) },
                     new ButtonItem { Content = "切换机型", ClickCommand = new RelayCommand(() => ChangeMachineTypeExecute()) },
                    
                },
                new ObservableCollection<ButtonItem>
                {
                    new ButtonItem { Content = "580 CFG1授权码", ClickCommand = new RelayCommand(() => CopyQ580Cfg1EXecute()) },
                    new ButtonItem { Content = "750 CFG1授权码", ClickCommand = new RelayCommand(() => CopyT750Cfg1EXecute()) },
                    new ButtonItem { Content = "宠物 750 CFG1授权码", ClickCommand = new RelayCommand(() => CopyPet750Cfg1EXecute()) },
                    
                },
                 new ObservableCollection<ButtonItem>
                {
                   new ButtonItem { Content = "中文", ClickCommand = new RelayCommand(() => ChangeLanguage2Chinese()) },
                   new ButtonItem { Content = "英文", ClickCommand = new RelayCommand(() => ChangeLanguage2English()) },
                   new ButtonItem { Content = "俄文", ClickCommand = new RelayCommand(() => ChangeLanguage2Russian()) },
                },
                new ObservableCollection<ButtonItem>
                {
                   new ButtonItem { Content = "打开Master", ClickCommand = new RelayCommand(() => OpenFileExecute(pathConfigInfo.MasterPath)) },
                   new ButtonItem { Content = "工作站授权码", ClickCommand = new RelayCommand(() => CopyWorkStationExecute()) },
                   //new ButtonItem { Content = "自动登录", ClickCommand = new RelayCommand(() => SetAutoLogin()) },
                   //new ButtonItem { Content = "模拟器", ClickCommand = new RelayCommand(() => OpenSimulator()) },
                   new ButtonItem { Content = "kill monitor", ClickCommand = new RelayCommand(() => killmonitorExecute()) },
                }


            };
        }

        /// <summary>
        /// 打开模拟器
        /// </summary>
        private void OpenSimulator()
        {
            string _SourcePath = Path.Combine(pathConfigInfo.MasterPath, "Package-ServiceTool", "VersionConverter", "Convert2SimulatorALS.exe");
            string _DestPath = pathConfigInfo.UnpackagePath;
            string _DestFile = Path.Combine(pathConfigInfo.UnpackagePath, "Convert2SimulatorALS.exe");
            if (!File.Exists(_SourcePath))
            {
                InitMessageBox();
                _messagebox.Content = $"失败，路径{_SourcePath}不存在！";
                _messagebox.ShowDialogAsync();
                return;
            }
            try
            {
                File.Copy(_SourcePath, _DestFile, true);

                // 成功拷贝后，运行 exe
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = _DestFile,
                    CreateNoWindow = false, // 创建窗口
                    WorkingDirectory = _DestPath, // 可选，设置工作目录
                    UseShellExecute = false // 让它以系统默认方式打开
                };
                Process.Start(startInfo);
            }
            catch (Exception ex)
            {
                InitMessageBox();
                _messagebox.Content = $"出错：{ex.Message}";
                _messagebox.ShowDialogAsync();
            }
        }

        /// <summary>
        /// 切中文
        /// </summary>
        private void ChangeLanguage2Chinese()
        {
            string _path = Path.Combine(pathConfigInfo.UnpackagePath, "Configuration", "MultiLanguageConfig.json");
            string _path2 = Path.Combine(pathConfigInfo.UnpackagePath, "Configuration", "MiscellaneousConfig.json");

            if (!File.Exists(_path) || !File.Exists(_path2))
            {
                InitMessageBox();
                _messagebox.Content = $"失败，路径{_path}或{_path2}不存在！";
                _messagebox.ShowDialogAsync();
                return;
            }
            MultiLanguageConfig ret = FileOperation.ReadConfig<MultiLanguageConfig>(_path);
            MiscellaneousConfig ret2 = FileOperation.ReadConfig<MiscellaneousConfig>(_path2);

            ret.CurrentCulture = "zh-CN";
            ret2.SpecificCharacterSet = "ISO_IR 192";

            FileOperation.WriteConfig<MultiLanguageConfig>(_path,ret);
            FileOperation.WriteConfig<MiscellaneousConfig>(_path2, ret2);

            InitMessageBox();
            _messagebox.Content = "切换成功！";
            _messagebox.ShowDialogAsync();

        }

        /// <summary>
        /// 切英文
        /// </summary>
        private void ChangeLanguage2English()
        {
            string _path = Path.Combine(pathConfigInfo.UnpackagePath, "Configuration", "MultiLanguageConfig.json");
            string _path2 = Path.Combine(pathConfigInfo.UnpackagePath, "Configuration", "MiscellaneousConfig.json");
            if (!File.Exists(_path) || !File.Exists(_path2))
            {
                InitMessageBox();
                _messagebox.Content = $"失败，路径{_path}或{_path2}不存在！";
                _messagebox.ShowDialogAsync();
                return;
            }

            MultiLanguageConfig ret = FileOperation.ReadConfig<MultiLanguageConfig>(_path);
            MiscellaneousConfig ret2 = FileOperation.ReadConfig<MiscellaneousConfig>(_path2);

            ret.CurrentCulture = "en-US";
            ret2.SpecificCharacterSet = "ISO_IR 100";

            FileOperation.WriteConfig<MultiLanguageConfig>(_path, ret);
            FileOperation.WriteConfig<MiscellaneousConfig>(_path2, ret2);
            InitMessageBox();
            _messagebox.Content = "切换成功！";
            _messagebox.ShowDialogAsync();
        }

        /// <summary>
        /// 切俄文
        /// </summary>
        private void ChangeLanguage2Russian()
        {
            string _path = Path.Combine(pathConfigInfo.UnpackagePath, "Configuration", "MultiLanguageConfig.json");
            string _path2 = Path.Combine(pathConfigInfo.UnpackagePath, "Configuration", "MiscellaneousConfig.json");

            if (!File.Exists(_path) || !File.Exists(_path2))
            {
                InitMessageBox();
                _messagebox.Content = $"失败，路径{_path}或{_path2}不存在！";
                _messagebox.ShowDialogAsync();
                return;
            }
            MultiLanguageConfig ret = FileOperation.ReadConfig<MultiLanguageConfig>(_path);
            MiscellaneousConfig ret2 = FileOperation.ReadConfig<MiscellaneousConfig>(_path2);

            ret.CurrentCulture = "ru-RU";
            ret2.SpecificCharacterSet = "ISO_IR 100";

            FileOperation.WriteConfig<MultiLanguageConfig>(_path, ret);
            FileOperation.WriteConfig<MiscellaneousConfig>(_path2, ret2);
            InitMessageBox();
            _messagebox.Content = "切换成功！";
            _messagebox.ShowDialogAsync();
        }

        /// <summary>
        /// 此方法可能会导致WorkToolsSln.exe不能彻底退出
        /// </summary>
        private void killmonitorExecute()
        {
            string batpath = @"C:\Users\Administrator\Desktop\test-killer.bat";
            if(File.Exists(batpath))
            {
                try
                {
                    Process _process = new Process();
                    _process.StartInfo.FileName = batpath;
                    _process.StartInfo.CreateNoWindow = true;
                    _process.StartInfo.UseShellExecute = false;

                    _process.Start();

                    //_process.WaitForExit();//执行会卡死

                }
                catch (Exception ex)
                {
                    DBOperation.Instance.AddRecord("Error executing .bat file: " + ex.Message);
                }
            }
            else
            {
                System.Windows.MessageBox.Show("test-killer.bat 文件不存在，请检查！");
            }
        }

        private void SetAutoLogin()
        {
            string autoloninPath = pathConfigInfo.UnpackagePath + @"\Configuration\AutoLogin.txt";
            if (!File.Exists(autoloninPath))
            {
                MessageBox.Show("AutoLogin.txt文件不存在，请检查！");
                return;
            }

            InitMessageBox();
            string fileContent = File.ReadAllText(autoloninPath);
            if (fileContent.Contains("0"))
            {
                
                fileContent = fileContent.Replace("0","1");
                File.WriteAllText(autoloninPath, fileContent);
                _messagebox.ShowDialogAsync();
            }
        }

        private void InitMessageBox()
        {
            // 创建自定义 MessageBox
            _messagebox = new Wpf.Ui.Controls.MessageBox
            {
                Title = "提示",
                Content = "执行成功！",
                Width = 400, // 设置宽度
                Height = 200, // 设置高度
                MinWidth = 400, // 强制最小宽度
                MinHeight = 200, // 强制最小高度
                //HorizontalAlignment = HorizontalAlignment.Center,
                //VerticalAlignment = VerticalAlignment.Center,
                WindowStartupLocation = WindowStartupLocation.CenterOwner // 在父窗口的中心
            };
            // 设置 Owner 确保在应用程序窗口居中
            if (System.Windows.Application.Current.MainWindow != null)
            {
                _messagebox.Owner = System.Windows.Application.Current.MainWindow;
            }
        }

        private void InitCommand()
        {
            DependencyUpataCmd = new RelayCommand(DependencyUpdataExecute);
            UnpackageUpataCmd = new RelayCommand(UnpackageUpataExecute);
            UnpackageCleanUpCmd = new RelayCommand(UnpackageCleanUpExecute);
            UnpackageCheckoutCmd = new RelayCommand(UnpackageCheckoutExecute);
            Denpenency2UnpackageCmd = new RelayCommand(Denpenency2UnpackageExecute);
            DependencyRevertCmd = new RelayCommand(DependencyRevertExecute);
        }

        private void DependencyRevertExecute()
        {
            if (SvnRevert(pathConfigInfo.DependencyPath))
            {
                InitMessageBox();
                _messagebox.Content = "Revert Dependency 成功！";
                _messagebox.ShowDialogAsync();
                DBOperation.Instance.AddRecord($"Revert Dependency，路径:{pathConfigInfo.DependencyPath}");
            }
        }

        private void OpenFileExecute(string path)
        {
            try
            {
                if (Directory.Exists(path)) // 检查文件夹是否存在
                {
                    Process.Start(new ProcessStartInfo
                    {
                        FileName = path,
                        UseShellExecute = true, // 使用操作系统的外壳程序打开文件夹
                        Verb = "open"           // 指定操作为“打开”
                    });
                }
                else
                {
                    MessageBox.Show("指定的文件夹不存在，请检查路径！", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"打开文件夹失败：{ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #region Execute 方法

        private void ChangeMachineTypeExecute()
        {
            var result = System.Windows.MessageBox.Show($"确定切换机型吗？", "确认", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != System.Windows.MessageBoxResult.Yes)
            {
                return ;
            }

            InitMessageBox();
            if (!DeleteFolderContainingString(pathConfigInfo.UnpackagePath, "Configuration_"))
            {
                return;
            }

            RemoveReadOnlyAndDelete(Path.Combine(pathConfigInfo.UnpackagePath, "Configuration"),false);

            if(!SvnUpdata(Path.Combine(pathConfigInfo.UnpackagePath, "Configuration")))
            {
                return;
            }

            _messagebox.Content = "更换机型成功，请启动软件！";
            _messagebox.ShowDialogAsync();
        }

        private void DeleteDocumentExecute()
        {

            if(FileDeleteFunc(pathConfigInfo.UnpackagePath))
            {
                InitMessageBox();
                _messagebox.Content = "删除Unpackage 成功！";
                _messagebox.ShowDialogAsync();
                DBOperation.Instance.AddRecord($"删除Unpackage，路径:{pathConfigInfo.UnpackagePath}");
            }
        }

        private void UnpackageCheckoutExecute()
        {
            // 确认操作
            var result = System.Windows.MessageBox.Show($"确定Checkout？", "确认", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != System.Windows.MessageBoxResult.Yes)
            {
                return;
            }

            if (SvnCheckOutFunc(pathConfigInfo.UnpackagePath, pathConfigInfo.SvnURL))
            {
                InitMessageBox();
                _messagebox.Content = "检出Unpackage 成功！";
                _messagebox.ShowDialogAsync();
                DBOperation.Instance.AddRecord($"检出Unpackage，地址:{pathConfigInfo.SvnURL}");
            }
        }

        /// <summary>
        /// 合成包
        /// </summary>
        private void Denpenency2UnpackageExecute()
        {
            // 执行文件复制功能
            if(FileCopyFunc(pathConfigInfo.DependencyPath, pathConfigInfo.UnpackagePath))
            {
                InitMessageBox();
                _messagebox.Content = "合成包 成功！";
                _messagebox.ShowDialogAsync();
            }
            DBOperation.Instance.AddRecord($"合成软件包,{pathConfigInfo.DependencyPath}和{pathConfigInfo.UnpackagePath}");
        }

        /// <summary>
        /// 先Revert再Updata目录
        /// </summary>
        private void DependencyUpdataExecute()
        {
            if(SvnRevert(pathConfigInfo.DependencyPath))
            {
                if (SvnUpdata(pathConfigInfo.DependencyPath))
                {
                    InitMessageBox();
                    _messagebox.Content = "更新Dependency 成功！";
                    _messagebox.ShowDialogAsync();
                    DBOperation.Instance.AddRecord($"更新Dependency,路径:{pathConfigInfo.DependencyPath}");
                }
            }
        }

        /// <summary>
        /// 先Revert再Updata目录
        /// </summary>
        private void UnpackageUpataExecute()
        {
            // 确认操作
            var result = System.Windows.MessageBox.Show($"确定更新？", "确认", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != System.Windows.MessageBoxResult.Yes)
            {
                return;
            }

            if (SvnRevert(pathConfigInfo.UnpackagePath))
            {
                if (SvnUpdata(pathConfigInfo.UnpackagePath))
                {
                    InitMessageBox();
                    _messagebox.Content = "更新Unpackage 成功！";
                    _messagebox.ShowDialogAsync();
                    DBOperation.Instance.AddRecord($"更新Unpackage,路径:{pathConfigInfo.UnpackagePath}");
                }
            }

        }

        /// <summary>
        /// 清理目录
        /// </summary>
        private void UnpackageCleanUpExecute()
        {
            if(SVNCleanUp(pathConfigInfo.UnpackagePath))
            {
                InitMessageBox();
                _messagebox.Content = "清理Unpackage 成功！";
                _messagebox.ShowDialogAsync();
                DBOperation.Instance.AddRecord($"清理Unpackage,路径:{pathConfigInfo.UnpackagePath}");
            }
        }

        private void CopyQ580Cfg1EXecute()
        {
            System.Windows.Clipboard.SetText(pathConfigInfo.Q580Cfg1);
        }

        private void CopyT750Cfg1EXecute()
        {
            System.Windows.Clipboard.SetText(pathConfigInfo.T750Cfg1);
        }

        private void CopyPet750Cfg1EXecute()
        {
            System.Windows.Clipboard.SetText(pathConfigInfo.Pet750Cfg1);
        }

        private void CopyWorkStationExecute()
        {
            // 创建一个随机数生成器
            Random random = new Random();

            // 生成一个1或2的随机数
            int randomNumber = random.Next(1, 3); // 生成1或2

            if (randomNumber == 1)
            {
                System.Windows.Clipboard.SetText(pathConfigInfo.workStation.Code);
                System.Windows.MessageBox.Show("当前复制的是授权码");
            }
            else
            {
                System.Windows.Clipboard.SetText(pathConfigInfo.workStation.PassWord);
                System.Windows.MessageBox.Show("当前复制的是密钥");
            }
        }

        private void StartKillerExecute()
        {
            string exePath = Path.Combine(pathConfigInfo.UnpackagePath, "KKKiller.exe");
            string processName = "KKKiller";
            if (!File.Exists(exePath))
            {
                System.Windows.MessageBox.Show("Killer程序不存在！"); return;
            }
            try
            {
                // 查找已运行的进程
                Process existingProcess = Process.GetProcessesByName(processName).FirstOrDefault();

                if (existingProcess != null)
                {
                    // 如果程序已经启动，将窗口置于最前并居中
                    IntPtr hWnd = existingProcess.MainWindowHandle;

                    if (hWnd != IntPtr.Zero)
                    {
                        // 恢复窗口并置于最前
                        SystemHookHelper.ShowWindow(hWnd, SystemHookHelper.SW_RESTORE);
                        SystemHookHelper.SetForegroundWindow(hWnd);

                        // 获取屏幕大小，使用 System.Windows.Forms失败，需要用别的方法获取屏幕大小
                        //int screenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
                        //int screenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
                        int screenWidth = 3840;
                        int screenHeight = 2160;
                        // 获取窗口大小
                        SystemHookHelper.GetWindowRect(hWnd, out SystemHookHelper.RECT rect);
                        int windowWidth = rect.Right - rect.Left;
                        int windowHeight = rect.Bottom - rect.Top;

                        // 计算居中位置
                        int x = (screenWidth - windowWidth) / 2;
                        int y = (screenHeight - windowHeight) / 2;

                        // 移动窗口到屏幕中心
                        SystemHookHelper.MoveWindow(hWnd, x, y, windowWidth, windowHeight, true);
                    }
                }
                else
                {
                    // 如果程序未启动，则启动程序
                    Process.Start(exePath);
                    Console.WriteLine("程序已启动！");
                }
                DBOperation.Instance.AddRecord($"启动KKKiler, 路径：{exePath}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("启动KKKiller时发生错误！");
            }

        }

        private void StartExeExecute(string path)
        {
            //string m_path = Path.Combine(pathConfigInfo.UnpackagePath, "Vanguard.exe");
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = path,       // 可执行文件路径
                    UseShellExecute = true, // 必须启用 Shell 执行
                    Verb = "runas"         // 以管理员身份运行
                };

                // 启动程序
                Process.Start(startInfo);
                //DBManger.Instance.AddRecord();
                DBOperation.Instance.AddRecord($"启动软件，路径：{path}");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("启动失败");
            }
        }
        #endregion

        private bool SvnRevert(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                System.Windows.MessageBox.Show("文件夹为空，请检查！");
                return false;
            }
            if (!Directory.Exists(path))
            {
                System.Windows.MessageBox.Show("文件夹为空，请检查！");
                return false;
            }
            try
            {
                using (SvnClient client = new SvnClient())
                {
                    SvnRevertArgs revertArgs = new SvnRevertArgs
                    {
                        Depth = SvnDepth.Infinity  // 递归到所有子目录和文件
                    };

                    client.Revert(path, revertArgs);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Svn损坏，请检查！");
                return false;
            }
            return true;
        }

        private bool SvnUpdata(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                System.Windows.MessageBox.Show("路径为空，请检查配置文件！");
                return false;
            }
            if(!Directory.Exists(path))
            {
                System.Windows.MessageBox.Show("文件夹为空，请检查！");
                return false;
            }
            try
            {
                using (SvnClient client = new SvnClient())
                {

                    client.Update(path);

                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Svn损坏，请检查！");
                return false;
            }
            return true;
        }

        private bool SVNCleanUp(string Path)
        {
            try
            {
                using (SvnClient client = new SvnClient())
                {
                    bool result = client.CleanUp(Path);

                    // 弹窗提示任务状态
                    if (result)
                    {
                        return true;
                    }
                    else
                    {
                        System.Windows.MessageBox.Show("SVN Clean Up 失败，请检查路径或权限。", "操作失败", System.Windows.MessageBoxButton.OK, MessageBoxImage.Error);
                        return false;
                    }
                }
            }
            catch
            {
                System.Windows.MessageBox.Show("Clean Up 失败，请检查SVN状态！");
                return false;
            }
        }

        private bool SvnCheckOutFunc(string path, string url)
        {
            if (System.IO.Directory.Exists(path))
            {
                System.Windows.MessageBox.Show("请先删除Unpackage目录！");
                return false;
            }

            Directory.CreateDirectory(path);

            using (SvnClient client = new SvnClient())
            {
                // 配置 SVN 凭据
                //client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(username, password);

                // 执行 SVN checkout
                SvnCheckOutArgs args = new SvnCheckOutArgs();
                client.CheckOut(new Uri(url), path, args);
            }
            return true;
        }

        private bool FileCopyFunc(string source, string dest)
        {
            // 检查源文件夹是否存在
            if (!Directory.Exists(source))
            {
                System.Windows.MessageBox.Show($"{source} 文件夹不存在！");
                return false;
            }

            // 如果目标文件夹不存在，则创建
            if (!Directory.Exists(dest))
            {
                Directory.CreateDirectory(dest);
            }

            try
            {
                // 获取源文件夹中的所有文件
                string[] files = Directory.GetFiles(source);
                foreach (string file in files)
                {
                    // 跳过隐藏文件
                    FileInfo fileInfo = new FileInfo(file);
                    if ((fileInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue; // 跳过隐藏文件
                    }

                    // 获取文件名
                    string fileName = Path.GetFileName(file);

                    // 构建目标路径
                    string destFile = System.IO.Path.Combine(dest, fileName);

                    // 复制文件到目标文件夹
                    File.Copy(file, destFile, true);
                }

                // 如果需要递归复制子文件夹
                string[] directories = Directory.GetDirectories(source);
                foreach (string directory in directories)
                {
                    // 跳过隐藏文件夹
                    DirectoryInfo dirInfo = new DirectoryInfo(directory);
                    if ((dirInfo.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden)
                    {
                        continue; // 跳过隐藏文件夹
                    }

                    // 获取子文件夹的名称
                    string dirName = Path.GetFileName(directory);

                    // 构建目标子文件夹路径
                    string destDir = Path.Combine(dest, dirName);

                    // 递归复制子文件夹
                    FileCopyFunc(directory, destDir);
                }

                //MessageBox.Show($"已成功将 {source} 中的文件复制到 {dest}！");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"文件复制过程中发生错误: {ex.Message}");
                return false;
            }
            return true;
        }

        private bool FileDeleteFunc(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                System.Windows.MessageBox.Show($"{path}文件夹不存在！");
                return false;
            }

            // 确认删除操作
            var result = System.Windows.MessageBox.Show($"确定要删除 {path} 及其所有内容吗？", "确认删除", System.Windows.MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result != System.Windows.MessageBoxResult.Yes)
            {
                return false;
            }

            try
            {
                // 强制删除目录及其内容
                RemoveReadOnlyAndDelete(path);
            }
            catch (Exception ex)
            {
                // 显示异常信息
                System.Windows.MessageBox.Show($"删除失败：{ex.Message}");
            }
            return true;
        }

        private void RemoveReadOnlyAndDelete(string path,bool isDeleteDirectory = true)
        {
            if (File.Exists(path))
            {
                // 取消文件的隐藏和只读属性
                File.SetAttributes(path, FileAttributes.Normal);
                File.Delete(path); // 删除文件
            }
            else if (Directory.Exists(path))
            {
                // 遍历所有文件和子目录
                foreach (var file in Directory.GetFiles(path))
                {
                    RemoveReadOnlyAndDelete(file);
                }
                foreach (var dir in Directory.GetDirectories(path))
                {
                    RemoveReadOnlyAndDelete(dir);
                }

                if(isDeleteDirectory)
                {
                    // 删除空目录
                    Directory.Delete(path, true);
                }
            }
            else
            {
                System.Windows.MessageBox.Show($"路径 {path} 不存在！");
            }
        }

        // 查找包含特定字符的文件夹并删除
        public bool DeleteFolderContainingString(string directoryPath, string searchString)
        {
            if (string.IsNullOrWhiteSpace(directoryPath) || string.IsNullOrWhiteSpace(searchString))
            {
                MessageBox.Show("路径或搜索字符串不能为空!");
                return false;
            }

            try
            {
                var directories = Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly);
                foreach (var dir in directories)
                {
                    if (dir.Contains(searchString))
                    {
                        Directory.Delete(dir, true);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"请检查文件{directoryPath}是否被占用或不存在!");
                return false;
            }
            return true;
        }

        ///// <summary>
        ///// 删除指定文件夹内的文件
        ///// </summary>
        ///// <param name="Directorypath"></param>
        //private bool DeleteDirectoryContentFile(string Directorypath)
        //{
        //    try
        //    {
        //        if (Directory.Exists(Directorypath))
        //        {
        //            foreach (var dir in Directory.GetDirectories(Directorypath))
        //            {
        //                if (File.Exists(dir))
        //                {
        //                    // 取消文件的隐藏和只读属性
        //                    File.SetAttributes(dir, FileAttributes.Normal);
        //                    File.Delete(dir); // 删除文件
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show($"删除文件夹内文件失败：{ex.Message}");
        //        return false;
        //    }

        //    return true;
        //}
    }
}