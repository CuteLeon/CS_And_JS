using System;
using System.Windows;
using System.Windows.Data;
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

            this.TabWebBrowser.SetBinding(ChromiumWebBrowser.AddressProperty, new Binding(nameof(this.SourceUri)) { Source = this });
            this.TabWebBrowser.SetBinding(ChromiumWebBrowser.TitleProperty, new Binding(nameof(this.Title)) { Source = this, Mode = BindingMode.OneWayToSource });
        }

        public ChromiumWebBrowser TabWebBrowser { get; set; }

        public string Title { get => (string)this.GetValue(TitleProperty); set => this.SetValue(TitleProperty, value); }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(nameof(Title), typeof(string), typeof(TabViewModel), new PropertyMetadata("新标签"));

        public Uri SourceUri { get => (Uri)this.GetValue(SourceUriProperty); set => this.SetValue(SourceUriProperty, value); }

        public static readonly DependencyProperty SourceUriProperty = DependencyProperty.RegisterAttached(nameof(SourceUri), typeof(Uri), typeof(TabViewModel), new PropertyMetadata(null));
    }
}
