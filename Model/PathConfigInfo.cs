using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkToolsSln.Model
{
    public class PathConfigInfo
    {
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
    }
}
