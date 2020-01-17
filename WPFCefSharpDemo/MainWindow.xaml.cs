using System;
using System.Linq;
using System.Windows;
using CefSharp;
using CefSharp.Wpf;
using WPFCefSharpDemo.ViewModels;

namespace WPFCefSharpDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 视图模型
        /// </summary>
        public ContainerViewModel ViewModel { get; set; }

        public MainWindow()
        {
            CefSettings settings = new CefSettings()
            {
                BackgroundColor = 0xF00,
            };
            Cef.Initialize(settings);

            this.InitializeComponent();

            this.ViewModel = new ContainerViewModel();
            this.ViewModel.Tabs.Add(new TabViewModel() { Title = "默认标签名称", SourceUri = new Uri("https://www.zhihu.com") });
            this.ViewModel.Tabs.Add(new TabViewModel() { Title = "默认标签名称", SourceUri = new Uri("https://www.baidu.com") });
            this.ViewModel.CurrentTab = this.ViewModel.Tabs.FirstOrDefault();

            this.DataContext = this.ViewModel;
        }
    }
}
