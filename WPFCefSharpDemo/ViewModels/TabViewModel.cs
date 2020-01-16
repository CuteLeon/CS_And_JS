using System;
using System.Windows;

namespace WPFCefSharpDemo.ViewModels
{
    public class TabViewModel : DependencyObject
    {
        public Uri SourceUri { get => (Uri)this.GetValue(SourceUriProperty); set => this.SetValue(SourceUriProperty, value); }

        public static readonly DependencyProperty SourceUriProperty = DependencyProperty.RegisterAttached(nameof(SourceUri), typeof(Uri), typeof(TabViewModel), new PropertyMetadata(null));
    }
}
