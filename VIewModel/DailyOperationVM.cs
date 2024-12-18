using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SharpSvn;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;
using WorkToolsSln.VIewModel;
using WorkToolsSln.Helper;
using WorkToolsSln.Model;
using Wpf.Ui;
using System.ComponentModel;
using SharpSvn.UI;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace WorkToolsSln.VIewModel
{
    public sealed partial class DailyOperationVM : ViewModel
    {
        private readonly INavigationService _navigationService;
        private PathConfigInfo pathConfigInfo { get; set; }
        private readonly string ConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Configuration", "PathConfig.json");

        public ObservableCollection<ButtonItem> ButtonList;

        public DailyOperationVM(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitCommand();
            InitUI();
            pathConfigInfo = FileOperation.ReadConfig<PathConfigInfo>(ConfigPath);
        }

        private void InitUI()
        {
            ButtonList = new ObservableCollection<ButtonItem>
            {
                new ButtonItem { Content = "按钮1", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮1被点击")) },
                new ButtonItem { Content = "按钮2", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮2被点击")) },
                new ButtonItem { Content = "按钮3", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮3被点击")) },
                new ButtonItem { Content = "按钮4", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮4被点击")) },
                new ButtonItem { Content = "按钮5", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮5被点击")) },
                new ButtonItem { Content = "按钮6", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮6被点击")) },
                new ButtonItem { Content = "按钮7", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮7被点击")) },
                new ButtonItem { Content = "按钮8", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮8被点击")) },
                new ButtonItem { Content = "按钮9", ClickCommand = new RelayCommand(() => MessageBox.Show("按钮9被点击")) },
            };
        }

        private void InitCommand()
        {
            DependencyUpataCmd = new RelayCommand(DependencyUpdataExecute);
            UnpackageUpataCmd = new RelayCommand(UnpackageUpataExecute);
            UnpackageCleanUpCmd = new RelayCommand(UnpackageCleanUpExecute);
            UnpackageCheckoutCmd = new RelayCommand(UnpackageCheckoutExecute);
            Denpenency2UnpackageCmd = new RelayCommand(Denpenency2UnpackageExecute);
            DeleteDocumentCmd = new RelayCommand(DeleteDocumentExecute);
        }

        private void DeleteDocumentExecute()
        {
            FileDeleteFunc(pathConfigInfo.UnpackagePath);
        }

        private void UnpackageCheckoutExecute()
        {
            SvnCheckOutFunc(pathConfigInfo.UnpackagePath, pathConfigInfo.SvnURL);
        }

        private void Denpenency2UnpackageExecute()
        {
            FileCopyFunc(pathConfigInfo.DependencyPath,pathConfigInfo.UnpackagePath);
        }

        /// <summary>
        /// 清理目录
        /// </summary>
        private void UnpackageCleanUpExecute()
        {
            SVNCleanUp(pathConfigInfo.UnpackagePath);
        }

        public ICommand DependencyUpataCmd { get; protected set; }
        public ICommand UnpackageUpataCmd { get; protected set; }
        public ICommand UnpackageCleanUpCmd { get; protected set; }
        public ICommand Denpenency2UnpackageCmd { get; protected set; }
        public ICommand UnpackageCheckoutCmd { get; protected set; }
        public ICommand DeleteDocumentCmd { get; protected set; }

        /// <summary>
        /// 先Revert再Updata目录
        /// </summary>
        private void DependencyUpdataExecute()
        {
            SvnRevert(pathConfigInfo.DependencyPath);
            SvnUpdata(pathConfigInfo.DependencyPath);
        }

        /// <summary>
        /// 先Revert再Updata目录
        /// </summary>
        private void UnpackageUpataExecute()
        {
            SvnRevert(pathConfigInfo.UnpackagePath);
            SvnUpdata(pathConfigInfo.UnpackagePath);
        }

        //private void 
       
        private void SvnRevert(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("文件夹为空，请检查！");
                return;
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

            }
        }

        private void SvnUpdata(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                MessageBox.Show("文件夹为空，请检查！");
                return;
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

            }
        }

        private void SVNCleanUp(string Path)
        {
            using (SvnClient client = new SvnClient())
            {
                bool result = client.CleanUp(Path);

                // 弹窗提示任务状态
                if (result)
                {
                    MessageBox.Show("SVN Clean Up 成功完成！", "操作成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("SVN Clean Up 失败，请检查路径或权限。", "操作失败", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void SvnCheckOutFunc(string path,string url)
        {
            if(System.IO.Directory.Exists(path))
            {
                MessageBox.Show("请先删除Unpackage目录！");
            }

            using (SvnClient client = new SvnClient())
            {
                // 配置 SVN 凭据
                //client.Authentication.DefaultCredentials = new System.Net.NetworkCredential(username, password);

                // 检查是否存在工作目录
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }

                // 执行 SVN checkout
                SvnCheckOutArgs args = new SvnCheckOutArgs();
                client.CheckOut(new Uri(url), path, args);
            }
        }

        private void FileCopyFunc(string source, string dest)
        {
            if (!System.IO.Directory.Exists(source))
            {
                MessageBox.Show($"{source}文件夹不存在！");
                return;
            }
            if(!System.IO.Directory.Exists(dest))
            {
                MessageBox.Show($"{dest}文件夹不存在！");
                return;
            }

            File.Copy(source, dest,true);
        }

        private void FileDeleteFunc(string path)
        {
            if (!System.IO.Directory.Exists(path))
            {
                MessageBox.Show($"{path}文件夹不存在！");
                return;
            }

            System.IO.Directory.Delete(path,true);
        }
    }
}