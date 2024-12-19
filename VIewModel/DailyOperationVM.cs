using CommunityToolkit.Mvvm.ComponentModel;
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

namespace WorkToolsSln.VIewModel
{
    public sealed partial class DailyOperationVM : ViewModel
    {
        private readonly INavigationService _navigationService;
        private PathConfigInfo pathConfigInfo { get; set; }
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
        public ICommand DependencyUpataCmd { get; protected set; }
        public ICommand UnpackageUpataCmd { get; protected set; }
        public ICommand UnpackageCleanUpCmd { get; protected set; }
        public ICommand Denpenency2UnpackageCmd { get; protected set; }
        public ICommand UnpackageCheckoutCmd { get; protected set; }
        public ICommand DeleteDocumentCmd { get; protected set; }
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
                    new ButtonItem { Content = "打开Master", ClickCommand = new RelayCommand(() => OpenFileExecute(pathConfigInfo.MasterPath)) },
                    new ButtonItem { Content = "Killer", ClickCommand = new RelayCommand(() => StartKillerExecute()) },
                    
                },
                new ObservableCollection<ButtonItem>
                {
                    new ButtonItem { Content = "打开Visual Studio", ClickCommand = new RelayCommand(() => StartExeExecute(pathConfigInfo.VisualStudioPath)) },
                    new ButtonItem { Content = "删除Unpackage", ClickCommand = new RelayCommand(() => DeleteDocumentExecute()) },
                    new ButtonItem { Content = "580 CFG1授权码", ClickCommand = new RelayCommand(() => CopyQ580Cfg1EXecute()) },
                },
                new ObservableCollection<ButtonItem>
                {
                    new ButtonItem { Content = "750 CFG1授权码", ClickCommand = new RelayCommand(() => CopyT750Cfg1EXecute()) },
                    new ButtonItem { Content = "宠物 750 CFG1授权码", ClickCommand = new RelayCommand(() => CopyPet750Cfg1EXecute()) },
                    new ButtonItem { Content = "工作站授权码", ClickCommand = new RelayCommand(() => CopyWorkStationExecute()) }, 
                }
            };
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

        private void DeleteDocumentExecute()
        {

            if(FileDeleteFunc(pathConfigInfo.UnpackagePath))
            {
                InitMessageBox();
                _messagebox.Content = "删除Unpackage 成功！";
                _messagebox.ShowDialogAsync();
            }
        }

        private void UnpackageCheckoutExecute()
        {
            if(SvnCheckOutFunc(pathConfigInfo.UnpackagePath, pathConfigInfo.SvnURL))
            {
                InitMessageBox();
                _messagebox.Content = "检出Unpackage 成功！";
                _messagebox.ShowDialogAsync();
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
                }
            }
        }

        /// <summary>
        /// 先Revert再Updata目录
        /// </summary>
        private void UnpackageUpataExecute()
        {
            if(SvnRevert(pathConfigInfo.UnpackagePath))
            {
                if (SvnUpdata(pathConfigInfo.UnpackagePath))
                {
                    InitMessageBox();
                    _messagebox.Content = "更新Unpackage 成功！";
                    _messagebox.ShowDialogAsync();
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

                    client.Revert(path, new SvnRevertArgs());

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
                    string destFile = Path.Combine(dest, fileName);

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

        private void RemoveReadOnlyAndDelete(string path)
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

                // 删除空目录
                Directory.Delete(path,true);
            }
            else
            {
                System.Windows.MessageBox.Show($"路径 {path} 不存在！");
            }
        }

    }
}