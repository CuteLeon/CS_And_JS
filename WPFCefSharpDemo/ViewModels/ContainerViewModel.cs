using System.Collections.ObjectModel;
using System.Windows;

namespace WPFCefSharpDemo.ViewModels
{
    public class ContainerViewModel : DependencyObject
    {
        public TabViewModel CurrentTab { get => (TabViewModel)this.GetValue(CurrentTabProperty); set => this.SetValue(CurrentTabProperty, value); }
        public static readonly DependencyProperty CurrentTabProperty = DependencyProperty.RegisterAttached(nameof(CurrentTab), typeof(TabViewModel), typeof(ContainerViewModel), new PropertyMetadata(null));

        public ObservableCollection<TabViewModel> Tabs = new ObservableCollection<TabViewModel>();
    }
}
