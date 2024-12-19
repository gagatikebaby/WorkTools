using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkToolsSln.Model
{
    public class PathConfigInfo
    {
        public PathConfigInfo() => workStation = new WorkStation();

        public string VisualStudioPath { get; set; }

        public string MasterPath { get; set; }

        /// <summary>
        /// Dependency路径
        /// </summary>
        public string DependencyPath { get; set; }

        /// <summary>
        /// Unpackagel路径
        /// </summary>
        public string UnpackagePath { get; set; }

        /// <summary>
        /// SvnURL
        /// </summary>
        public string SvnURL { get; set; }

        /// <summary>
        /// 授权码1
        /// </summary>
        public string Q580Cfg1 { get; set; }

        /// <summary>
        /// 授权码2
        /// </summary>
        public string T750Cfg1 { get; set; }

        /// <summary>
        /// 授权码3
        /// </summary>
        public string Pet750Cfg1 { get; set; }

        /// <summary>
        /// WorkStation包含授权码和秘钥
        /// </summary>
        public WorkStation workStation { get; set; }
    }

    public class WorkStation
    {
        public WorkStation()
        {
            Code = string.Empty;
            PassWord = string.Empty;
        }
        public string Code { get; set; }
        public string PassWord { get; set; }
    }

}
