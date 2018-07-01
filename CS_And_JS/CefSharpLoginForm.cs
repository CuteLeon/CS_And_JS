using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CefSharp;
using CefSharp.Enums;
using CefSharp.Event;
using CefSharp.Handler;
using CefSharp.Internals;
using CefSharp.ModelBinding;
using CefSharp.SchemeHandler;
using CefSharp.Structs;
using CefSharp.WinForms;
using CefSharp.WinForms.Internals;
/* 使用 CefSharp 作为浏览器控件，需要.NetFramework版本为 >=4.5.2 且为x86平台，
 * 需要安装 CefSharp.Winform Nuget包，
 * 貌似不能直接从Stream读取网页内容，需要使用MainWebBrowser.RegisterResourceHandler()方法为网页流绑定地址，饭后访问这个地址，
 */

namespace CS_And_JS
{
    public partial class CefSharpLoginForm : Form
    {
        ChromiumWebBrowser MainWebBrowser;

        public CefSharpLoginForm()
        {
            InitializeComponent();

            //TODO: 允许绑定JS对象
            CefSharpSettings.LegacyJavascriptBindingEnabled = true;

            //TODO: 此处构造不可使用无参构造程序，否则下面会要求为IBrowser注册资源句柄；
            MainWebBrowser = new ChromiumWebBrowser("");

            //TODO: 为浏览器注册自定义流到指定的网址，并访问以显示自定义的流
            MainWebBrowser.RegisterResourceHandler("http://www.LoginForm.com", GetHTMLStream(), "text/html");
            MainWebBrowser.Load("http://www.LoginForm.com");
            //TODO: 注册JS对象并命名，JS中使用此命名调用对象方法，最后赋值为false是为了忽略方法大小写，否则只能调用小写字母开头的方法
            MainWebBrowser.RegisterJsObject("LoginHost", this, new BindingOptions() {  CamelCaseJavascriptNames = false});
            
            //禁用右键菜单
            MainWebBrowser.MenuHandler = new DisableMenuHandler();
            MainWebBrowser.Parent = this;
            MainWebBrowser.Dock = DockStyle.Fill;
        }

        private MemoryStream GetHTMLStream()
        {
            #region 网页内容
            string HTMLContent = @"
﻿<!DOCTYPE html>
<html >
  <head>
    <meta charset=""UTF-8"">
    <title>Cute Leon.</title>
    
    <style>
      body{
        background-color: #fa6c9f;
      }
      h1{
        font-size: 40px;
        font-family:""Microsoft YaHei"";
        color: #fff;
        text-shadow: 0px 0px 50px #fa6c9f;
      }
      /* 按钮文本区域样式 */
      span {
        font:bold;
        font-family:""Microsoft YaHei"";
        display: inline-block;
        padding: 18px 60px;
        border-radius: 50em;
        position: relative;
        z-index: 2;
        border: 5px solid #fff;
      }
      /* 按钮填充和阴影样式 */
      .bg-gradient span,
      .bg-gradient:before {
        background: #fa6c9f;
        background: -webkit-linear-gradient(left, #fa6c9f 0%, #ffe140 80%, #ffe140 100%);
        background: linear-gradient(to right, #fa6c9f 0%, #ffe140 80%, #ffe140 100%);
      }
      .wrapper {
        position: relative;
        text-align: center;
      }
      
      .fancy-button {
        display: inline-block;
        margin: 30px;
        font-family: 'Montserrat', Helvetica, Arial, sans-serif;
        font-size: 30px;
        letter-spacing: 0.03em;
        text-transform: uppercase;
        color: #fff;
        position: relative;
      }
      
      .fancy-button:before {
        content: '';
        display: inline-block;
        height: 100px;
        width: 120%;
        position: absolute;
        bottom: -5px;
        left: -10%;
        right: 30px;
        z-index: -1;
        opacity: 0.5;
        bottom: -7px;
        -webkit-filter: blur(20px) brightness(1);
                filter: blur(20px) brightness(1);
        -webkit-transform-style: preserve-3d;
                transform-style: preserve-3d;
        -webkit-transition: all 0.3s ease-out;
        transition: all 0.3s ease-out;
      }
      
      .fancy-button:hover:before {
        bottom: -7px;
        opacity: 1;
        -webkit-filter: blur(30px);
                filter: blur(30px);
      }

      input{
	    transition:all 0.30s ease-in-out;
	    -webkit-transition: all 0.30s ease-in-out;
	    -moz-transition: all 0.30s ease-in-out;
	    height:30px;
        font-size:15px;
	    border:#35a5e5 1px solid;
	    border-radius:3px;
	    outline:none;
      }
      input:focus{
	    box-shadow:0 0 25px rgba(81, 203, 238, 1);
	    -webkit-box-shadow:0 0 25px rgba(81, 203, 238, 1);
	    -moz-box-shadow:0 0 25px rgba(81, 203, 238, 1);
      }
    </style>
  </head>

  <body>
    <div class=""wrapper"" style=""top: 20%;"">
      <h1>欢迎访问 Cute Leon.</h1>
      <input type=""text"" placeholder=""用户名"" id=""UserNameTextBox""/>
      <input type=""password"" placeholder=""密码"" id=""PasswordTextBox""/>
    </div>
    <div class=""wrapper"" style=""top: 40%;"">
      <a onclick=""CheckLogin();"" class=""fancy-button bg-gradient""><span id=""ButtonLabel"">   Welcome !   </span></a>
    </div>
  </body>

  <script type=""text/javascript"">
    function CheckLogin()
    {
        var UserName=document.getElementById(""UserNameTextBox"").value;
        var Password=document.getElementById(""PasswordTextBox"").value;
        LoginHost.CheckLogin(UserName, Password);
    }
    function LoginSuccessfully(message)
    {
        document.getElementById(""ButtonLabel"").innerText=message;
        window.setTimeout(GotoLeonNote, 1000); 
        }
        function GotoLeonNote()
        {
            document.getElementById(""ButtonLabel"").innerText=""正在跳转..."";
            window.location.href=""http://118.25.131.65:1304"";
        }
  </script>
</html>";
            #endregion

            return new MemoryStream(Encoding.UTF8.GetBytes(HTMLContent));
        }

        //TODO : 被 JS 调用的方法必须为 Public ;
        public void CheckLogin(string UserName, string Password)
        {
            if (UserName == "123" && Password == "456")
            {
                //TODO : Client 调用 Browser 代码；
                MainWebBrowser.ExecuteScriptAsync("LoginSuccessfully", new String[] { "登录成功，欢迎访问！" });
                /* 或使用 EvaluateScriptAsync 方法可以获取JS返回结果；
                var task = MainWebBrowser.EvaluateScriptAsync("LoginSuccessfully", "123");
                => task.Result
                 */
            }
            else
            {
                MessageBox.Show("用户名或密码输入错误，请重新输入！");
            }
        }

    }

    /// <summary>
    /// 禁用菜单事件
    /// </summary>
    public class DisableMenuHandler : IContextMenuHandler
    {
        void CefSharp.IContextMenuHandler.OnBeforeContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model) => model.Clear();
        bool CefSharp.IContextMenuHandler.OnContextMenuCommand(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.CefMenuCommand commandId, CefSharp.CefEventFlags eventFlags) => false;
        void CefSharp.IContextMenuHandler.OnContextMenuDismissed(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame) {}
        bool CefSharp.IContextMenuHandler.RunContextMenu(CefSharp.IWebBrowser browserControl, CefSharp.IBrowser browser, CefSharp.IFrame frame, CefSharp.IContextMenuParams parameters, CefSharp.IMenuModel model, CefSharp.IRunContextMenuCallback callback) => false;
    }
}
