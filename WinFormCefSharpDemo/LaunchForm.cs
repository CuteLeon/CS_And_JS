using System;
using System.Windows.Forms;

namespace WinFormCefSharpDemo
{
    public partial class LaunchForm : Form
    {
        public LaunchForm()
        {
            this.InitializeComponent();
        }

        private void webBrowserButton_Click(object sender, EventArgs e)
        {
            new WebBrowserLoginForm().Show(this);
        }

        private void cefSharpButton_Click(object sender, EventArgs e)
        {
            new CefSharpLoginForm().Show(this);
        }

        private void webViewButton_Click(object sender, EventArgs e)
        {
            new WebViewLoginForm().Show(this);
        }
    }
}
