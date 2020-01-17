using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using CefSharp.Wpf;
using WPFCefSharpDemo.Assists;

namespace WPFCefSharpDemo.ViewModels
{
    public class TabViewModel : DependencyObject
    {
        public TabViewModel()
        {
            this.TabWebBrowser = new ChromiumWebBrowser()
            {
                LifeSpanHandler = new CustomLifeSpanHandler(),
            };

            this.TabWebBrowser.SetBinding(ChromiumWebBrowser.AddressProperty, new Binding(nameof(this.SourceUri)) { Source = this, Mode = BindingMode.TwoWay });

            this.TabWebBrowser.SetBinding(ChromiumWebBrowser.TitleProperty, new Binding(nameof(this.Title)) { Source = this, Mode = BindingMode.OneWayToSource });
            this.TabWebBrowser.SetBinding(ChromiumWebBrowser.CanGoBackProperty, new Binding(nameof(this.CanGoBack)) { Source = this, Mode = BindingMode.OneWayToSource });
            this.TabWebBrowser.SetBinding(ChromiumWebBrowser.CanGoForwardProperty, new Binding(nameof(this.CanGoForward)) { Source = this, Mode = BindingMode.OneWayToSource });

            this.NavigateCommand = new DelegateCommand<string>((string uri) => this.SourceUri = new Uri(uri));

            this.BackCommand = this.TabWebBrowser.BackCommand;
            this.ForwardCommand = this.TabWebBrowser.ForwardCommand;
            this.ReloadCommand = this.TabWebBrowser.ReloadCommand;
            this.StopCommand = this.TabWebBrowser.StopCommand;
            this.ZoomInCommand = this.TabWebBrowser.ZoomInCommand;
            this.ZoomOutCommand = this.TabWebBrowser.ZoomOutCommand;
            this.ZoomResetCommand = this.TabWebBrowser.ZoomResetCommand;
        }

        #region 属性

        public bool CanGoBack { get => (bool)this.GetValue(CanGoBackProperty); set => this.SetValue(CanGoBackProperty, value); }
        public static readonly DependencyProperty CanGoBackProperty = DependencyProperty.RegisterAttached(nameof(CanGoBack), typeof(bool), typeof(TabViewModel), new PropertyMetadata(false));

        public bool CanGoForward { get => (bool)this.GetValue(CanGoFowardProperty); set => this.SetValue(CanGoFowardProperty, value); }
        public static readonly DependencyProperty CanGoFowardProperty = DependencyProperty.RegisterAttached(nameof(CanGoForward), typeof(bool), typeof(TabViewModel), new PropertyMetadata(false));

        public ChromiumWebBrowser TabWebBrowser { get; set; }

        public string Title { get => (string)this.GetValue(TitleProperty); set => this.SetValue(TitleProperty, value); }
        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(nameof(Title), typeof(string), typeof(TabViewModel), new PropertyMetadata("新标签"));

        public Uri SourceUri { get => (Uri)this.GetValue(SourceUriProperty); set => this.SetValue(SourceUriProperty, value); }
        public static readonly DependencyProperty SourceUriProperty = DependencyProperty.RegisterAttached(nameof(SourceUri), typeof(Uri), typeof(TabViewModel), new PropertyMetadata(null));
        #endregion

        #region 命令

        public DelegateCommand<string> NavigateCommand { get; protected set; }

        public ICommand BackCommand { get; protected set; }
        public ICommand ForwardCommand { get; protected set; }
        public ICommand ReloadCommand { get; protected set; }
        public ICommand StopCommand { get; protected set; }
        public ICommand ZoomInCommand { get; protected set; }
        public ICommand ZoomOutCommand { get; protected set; }
        public ICommand ZoomResetCommand { get; protected set; }
        #endregion
    }
}
