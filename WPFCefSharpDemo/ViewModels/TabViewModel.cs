using System;
using System.Windows;

namespace WPFCefSharpDemo.ViewModels
{
    public class TabViewModel : DependencyObject
    {
        public string Title { get => (string)this.GetValue(TitleProperty); set => this.SetValue(TitleProperty, value); }

        public static readonly DependencyProperty TitleProperty = DependencyProperty.RegisterAttached(nameof(Title), typeof(string), typeof(TabViewModel), new PropertyMetadata("新标签"));

        public Uri SourceUri { get => (Uri)this.GetValue(SourceUriProperty); set => this.SetValue(SourceUriProperty, value); }

        public static readonly DependencyProperty SourceUriProperty = DependencyProperty.RegisterAttached(nameof(SourceUri), typeof(Uri), typeof(TabViewModel), new PropertyMetadata(null));
    }
}
