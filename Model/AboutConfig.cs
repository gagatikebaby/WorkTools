
namespace WorkToolsSln.Model
{
    public class AboutConfig
    {

        /// <summary>
        /// 软件发布版本
        /// </summary>
        public string SoftwareRelease { get; set; }

        /// <summary>
        /// 重建机版本
        /// </summary>
        public string ReconVersion { get; set; }

        /// <summary>
        /// 选配软件版本
        /// </summary>
        public string OptionalSoftVersion { get; set; }

        /// <summary>
        /// 产品版本
        /// </summary>
        public string ProductVersion { get; set; }

        /// <summary>
        /// Host软件完整版本
        /// </summary>
        public string HostSoftFullVersion { get; set; }

        /// <summary>
        /// Acq软件完整版本
        /// </summary>
        public string AcqSoftFullVersion { get; set; }

        /// <summary>
        /// Recon软件完整版本
        /// </summary>
        public string ReconSoftFullVersion { get; set; }

        /// <summary>
        /// 设备识别号
        /// </summary>
        public string EquipmentIdentificationNumber { get; set; }

        public bool UpdateFlag { get; set; } = false;

        /// <summary>
        /// 软件名称
        /// </summary>
        public string SoftwareName { get; set; }

        /// <summary>
        /// 安装包名称
        /// </summary>
        public string InstallerName { get; set; }
        
    }
}
