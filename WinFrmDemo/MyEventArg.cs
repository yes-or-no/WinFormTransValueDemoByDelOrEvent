using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFrmDemo
{
    public class MyEventArg :EventArgs
    {
        //传递主窗体的数据信息
        public string Text { get; set; }
    }
}
