using System.Windows;
using CefSharp;
using CefSharp.Wpf;

namespace WPFCefSharpDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            CefSettings settings = new CefSettings()
            {
                BackgroundColor = 0xF00,
            };
            Cef.Initialize(settings);

            this.InitializeComponent();
        }
    }
}
