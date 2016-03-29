using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace SSmodel
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            //打开生料细度延时参数辨识模块
            //Application.Run(new FormIndentifyDelay());

            //打开生料细度模型训练模块
            //Application.Run(new Form2());

 
            //打开生料细度模型预测模块
            //Application.Run(new FormFrecast());

            //打开生料细度模型d登录模块
            Application.Run(new FormLogin());
            
        }
    }
}
