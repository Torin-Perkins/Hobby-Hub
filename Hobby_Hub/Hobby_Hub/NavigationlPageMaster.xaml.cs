using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Hobby_Hub
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationlPageMaster : ContentPage
    {
        public ListView ListView;

        public NavigationlPageMaster()
        {
            InitializeComponent();

            BindingContext = new NavigationlPageMasterViewModel();
            ListView = MenuItemsListView;
        }

        class NavigationlPageMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<NavigationlPageMasterMenuItem> MenuItems { get; set; }

            public NavigationlPageMasterViewModel()
            {
                MenuItems = new ObservableCollection<NavigationlPageMasterMenuItem>(new[]
                {
                    new NavigationlPageMasterMenuItem { Id = 0, Title = "Page 1" },
                    new NavigationlPageMasterMenuItem { Id = 1, Title = "Page 2" },
                    new NavigationlPageMasterMenuItem { Id = 2, Title = "Page 3" },
                    new NavigationlPageMasterMenuItem { Id = 3, Title = "Page 4" },
                    new NavigationlPageMasterMenuItem { Id = 4, Title = "Page 5" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}