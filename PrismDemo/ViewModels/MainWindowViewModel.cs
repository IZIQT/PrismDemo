using Prism.Commands;
using Prism.Mvvm;
using PrismDemo.Interface;
using PrismDemo.Model;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace PrismDemo.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public static int NumTab = 0;

        private ObservableCollection<ITab> tabViewModels;
        public ObservableCollection<ITab> TabViewModels
        {
            get => tabViewModels;
            set
            {
                tabViewModels = value;
                OnPropertyChanged(nameof(TabViewModels));
            }
        }

        public ICommand NewTabCommand { get; }
        public MainWindowViewModel()
        {
            TabViewModels = new ObservableCollection<ITab>();

            NewTabCommand = new DelegateCommand<object>(NewTabExecute);
            tabViewModels.CollectionChanged += Tabs_CollectionChanged;
            tabViewModels.Add(new DataTabModel());
        }

        private void Tabs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            ITab tab;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    tab = (ITab)e.NewItems[0];
                    tab.CloseRequested += OnTanCloseRequested;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    tab = (ITab)e.OldItems[0];
                    tab.CloseRequested -= OnTanCloseRequested;
                    break;
            }
        }

        private void OnTanCloseRequested(object sender, EventArgs e)
        {
            if (((DataTabModel)sender).DeleteEnable && NumTab > 1)
            {
                tabViewModels.Remove((ITab)sender);
                NumTab--;
            }

        }

        private void NewTabExecute(object obj)
        {
            if (NumTab < 10)
            {
                tabViewModels.Add(new DataTabModel());
            }
        }
    }
}
