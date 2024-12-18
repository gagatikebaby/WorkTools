using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WorkToolsSln.Model
{
    public class ButtonItem
    {
        public string Content { get; set; }

        public ICommand ClickCommand { get; set; }
    }
}
