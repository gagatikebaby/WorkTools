using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkToolsSln.Model
{
    public class MiscellaneousConfig
    {
        public string SpecificCharacterSet { get; set; } = "ISO_IR 192";
    }
    
    public class MultiLanguageConfig
    {
        public string CurrentCulture { get; set; } = "zh-CN";
    }
}
 