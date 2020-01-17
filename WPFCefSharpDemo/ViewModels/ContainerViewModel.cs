using System;
using System.Collections.ObjectModel;
using System.Windows;
using WPFCefSharpDemo.Assists;

namespace WPFCefSharpDemo.ViewModels
{
    public class ContainerViewModel : DependencyObject
    {
        public ContainerViewModel()
        {
            this.NewTabCommand = new DelegateCommand(() =>
            {
                this.Tabs.Add(this.CurrentTab = new TabViewModel() { Title = "新建标签", SourceUri = new Uri("https://www.baidu.com") });
            });

            this.CloseTabCommand = new DelegateCommand<TabViewModel>((tab) =>
            {
                int index = Tabs.IndexOf(tab);
                if (index == -1)
                {
                    return;
                }

                if (tab.TabWebBrowser != null)
                {
                    tab.TabWebBrowser.StopCommand.Execute(tab.TabWebBrowser);
                    tab.TabWebBrowser.Dispose();
                }

                this.Tabs.RemoveAt(index);

                if (this.CurrentTab == tab)
                {
                    if (this.Tabs.Count == 0)
                    {
                        this.CurrentTab = null;
                    }
                    else
                    {
                        index = Math.Min(index, this.Tabs.Count - 1);
                        this.CurrentTab = this.Tabs[index];
                    }
                }
            });
        }

        #region 属性

        public TabViewModel CurrentTab { get => (TabViewModel)this.GetValue(CurrentTabProperty); set => this.SetValue(CurrentTabProperty, value); }
        public static readonly DependencyProperty CurrentTabProperty = DependencyProperty.RegisterAttached(nameof(CurrentTab), typeof(TabViewModel), typeof(ContainerViewModel), new PropertyMetadata(null));

        public ObservableCollection<TabViewModel> Tabs { get; } = new ObservableCollection<TabViewModel>();
        #endregion

        #region 命令

        public DelegateCommand NewTabCommand { get; set; }

        public DelegateCommand<TabViewModel> CloseTabCommand { get; protected set; }
        #endregion
    }
}
