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
    /* TODO : <Button></Button>标签自动刷新页面：
     * 把 <Button></Button> 改为 <Button type="button"></Button>
     */
    //TODO : 必须设置COM可见
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]

    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //TODO : 网页内脚本代码访问的对象赋值为this，允许 JS 访问此窗口；
            MainWebBrowser.ObjectForScripting = this;
            //TODO : 禁用网页右键菜单
            MainWebBrowser.IsWebBrowserContextMenuEnabled = false;
            //TODO : 禁用滚动条
            MainWebBrowser.ScrollBarsEnabled = false;
            //TODO : 禁止显示对话框
            MainWebBrowser.ScriptErrorsSuppressed = false;
        }

        //TODO : 被 JS 调用的方法必须为 Public ;
        public void CheckLogin(string UserName, string Password)
        {
            if (MainWebBrowser.ReadyState != WebBrowserReadyState.Complete) return;
            if (UserName == "123" && Password == "456")
            {
                //TODO : Client 调用 Browser 代码；
                MainWebBrowser.Document.InvokeScript("LoginSuccessfully",
               new String[] { "登录成功，欢迎访问！" });
            }
            else
            {
                MessageBox.Show("用户名或密码输入错误，请重新输入！");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Application.DoEvents();
            MemoryStream HTMLStream = GetHTMLStream();
            if (HTMLStream == null)
            {
                MessageBox.Show("空的网页流，程序即将退出...");
                Application.Exit();
            }
            MainWebBrowser.DocumentStream = HTMLStream;
        }

        private MemoryStream GetHTMLStream()
        {
            string HTMLContent = @"<!DOCTYPE html>
<html lang=""zh"">
<head>
<meta charset=""UTF-8"">
<meta http-equiv=""X-UA-Compatible"" content=""IE=edge,chrome=1""> 
<meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
<title> Login </title>

<style type=""text/css"">
    .center{text-align: center;}
    .login-page {
      width: 360px;
      padding: 8% 0 0;
      margin: auto;
    }
    .form {
      position: relative;
      z-index: 1;
      background: #FFFFFF;
      max-width: 360px;
      margin: 0 auto 100px;
      padding: 45px;
      text-align: center;
      box-shadow: 0 0 20px 0 rgba(0, 0, 0, 0.2), 0 5px 5px 0 rgba(0, 0, 0, 0.24);
    }
    .form input {
      font-family: ""Roboto"", sans-serif;
      outline: 0;
      background: #f2f2f2;
      width: 100%;
      border: 0;
      margin: 0 0 15px;
      padding: 15px;
      box-sizing: border-box;
      font-size: 14px;
    }
    .form button {
      font-family: ""Microsoft YaHei"",""Roboto"", sans-serif;
      text-transform: uppercase;
      outline: 0;
      background: #4CAF50;
      width: 100%;
      border: 0;
      padding: 15px;
      color: #FFFFFF;
      font-size: 14px;
      -webkit-transition: all 0.3 ease;
      transition: all 0.3 ease;
      cursor: pointer;
    }
    .form button:hover,.form button:active,.form button:focus {
      background: #43A047;
    }
    .container {
      position: relative;
      z-index: 1;
      max-width: 300px;
      margin: 0 auto;
    }
    .container:before, .container:after {
      content: """";
      display: block;
      clear: both;
    }
    .container .info {
      margin: 50px auto;
      text-align: center;
    }
    .container .info h1 {
      margin: 0 0 15px;
      padding: 0;
      font-size: 36px;
      font-weight: 300;
      color: #1a1a1a;
    }
    .container .info span {
      color: #4d4d4d;
      font-size: 12px;
    }
    .container .info span a {
      color: #000000;
      text-decoration: none;
    }
    .container .info span .fa {
      color: #EF3B3A;
    }
    body {
      background: #76b852; /* fallback for old browsers */
      background: -webkit-linear-gradient(right, #76b852, #8DC26F);
      background: -moz-linear-gradient(right, #76b852, #8DC26F);
      background: -o-linear-gradient(right, #76b852, #8DC26F);
      background: linear-gradient(to left, #76b852, #8DC26F);
      font-family: ""Roboto"", sans-serif;
      -webkit-font-smoothing: antialiased;
      -moz-osx-font-smoothing: grayscale;      
    }
</style>

</head>
<body>
    <div class=""htmleaf-container"">
        <div id=""wrapper"" class=""login-page"">
          <div id=""login_form"" class=""form"">
            <form class=""login-form"">
              <input type=""text"" placeholder=""用户名"" id=""UserNameTextBox""/>
              <input type=""password"" placeholder=""密码"" id=""PasswordTextBox""/>
              <button type=""button"" id=""LoginButton"" onclick=""CheckLogin();"">登　录</button>
            </form>
          </div>
          <center>
             <div id=""mainpage"" style=""display:none"">恭喜，登录成功！</div>
         </center>
        </div>
    </div>

    <script type=""text/javascript"">
        function CheckLogin()
        {
            var UserName=document.getElementById(""UserNameTextBox"").value;
            var Password=document.getElementById(""PasswordTextBox"").value;
            window.external.CheckLogin(UserName, Password);
        }
        function LoginSuccessfully(message)
        {
            document.getElementById(""login_form"").style.display=""none"";//隐藏
            document.getElementById(""mainpage"").style.display = """";//显示
            alert(message);
        }
    </script>
    </body>
</html>";
            return new MemoryStream(Encoding.UTF8.GetBytes(HTMLContent));
        }

    }
}
