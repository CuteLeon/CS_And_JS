using System;
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

            this.ViewModel = new ContainerViewModel
            {
                CurrentTab = new TabViewModel() { SourceUri = new Uri("https://www.zhihu.com") }
            };

            this.ViewModel.Tabs.Add(new TabViewModel() { SourceUri = new Uri("https://www.a.com") });
            this.ViewModel.Tabs.Add(new TabViewModel() { SourceUri = new Uri("https://www.b.com") });
            this.ViewModel.Tabs.Add(new TabViewModel() { SourceUri = new Uri("https://www.c.com") });

            this.DataContext = this.ViewModel;
        }
    }
}
