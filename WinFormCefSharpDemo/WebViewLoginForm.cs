using System;
using System.Windows.Forms;

namespace WinFormCefSharpDemo
{
    public partial class WebViewLoginForm : Form
    {
        public WebViewLoginForm()
        {
            this.InitializeComponent();
        }

        private void WebViewLoginForm_Shown(object sender, EventArgs e)
        {
            this.MainWebView.Navigate("https://www.zhihu.com");
        }
    }
}
