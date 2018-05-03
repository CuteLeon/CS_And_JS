using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace CS_And_JS
{
    //TODO : 必须设置COM可见
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class Form1 : Form
    {
        string HTMLPath = string.Empty;

        public Form1()
        {
            InitializeComponent();
            this.FormClosing += delegate (object s,FormClosingEventArgs e) { if (File.Exists(HTMLPath)) File.Delete(HTMLPath); };
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO : 网页内脚本代码访问的对象赋值为this，允许 JS 访问此窗口；
            webBrowser1.ObjectForScripting = this;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            try
            {
                HTMLPath = SaveHTML();
                if (string.IsNullOrEmpty(HTMLPath))
                {
                    throw new Exception("空HTML文件路径");
                }
                if (!File.Exists(HTMLPath))
                {
                    throw new Exception("不存在的HTML文件路径");
                }
            } catch (Exception ex)
            {
                MessageBox.Show("另存HTML文件遇到异常：" + ex.Message);
                Application.Exit();
            }
            webBrowser1.Navigate(HTMLPath);
        }

        private string SaveHTML()
        {
            string HTMLPath = Path.GetTempFileName();
            string HTMLContent = @"
<!DOCTYPE HTML PUBLIC ""-\/\/W3C//DTD HTML 4.0 Transitional//EN"">
<html>
    <head>
    <meta charset=""utf-8""/>
    <title></title>
        <script type=""text/javascript"">
            function TestInJS(message) 
            {
                alert('网页内 JS 函数 : \n' + message); 
            }
        </script>
    </head>
    <body>
        <button onclick=""TestInJS('点击了网页内 JS 测试按钮')"">网页内 JS 代码测试</button>
        <!-- TODO : JS 内可通过 window.external.方法名称 访问 CS 内代码 -->
        <button onclick=""window.external.TestInCS('点击了网页内 CS 代码测试按钮')"">客户端内 CS 代码测试</button>
    </body>
</html>";
            File.WriteAllText(HTMLPath, HTMLContent);
            return HTMLPath;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (webBrowser1.ReadyState != WebBrowserReadyState.Complete) return;
            //TODO : Client 调用 Browser 代码；
            webBrowser1.Document.InvokeScript("TestInJS",
               new String[] { "点击了客户端内 JS 代码测试按钮" });
        }

        //TODO : 被 JS 调用的方法必须为 Public ;
        public void TestInCS(string Message)
        {
            this.Text = Message;
            MessageBox.Show(Message);
        }

    }
}
