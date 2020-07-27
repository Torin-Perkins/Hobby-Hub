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
public partial class AppMasterDetailPageMaster : ContentPage
{
    public ListView ListView;

    public AppMasterDetailPageMaster()
    {
        InitializeComponent();

        BindingContext = new AppMasterDetailPageMasterViewModel();
        ListView = MenuItemsListView;
    }

    class AppMasterDetailPageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<AppMasterDetailPageMasterMenuItem> MenuItems { get; set; }

        public AppMasterDetailPageMasterViewModel()
        {
            MenuItems = new ObservableCollection<AppMasterDetailPageMasterMenuItem>(new[]
            {
                    new AppMasterDetailPageMasterMenuItem { Id = 0, Title = "Home", TargetType = typeof(AppMasterDetailPageDetail)},
                    new AppMasterDetailPageMasterMenuItem { Id = 1, Title = "Art"},
                    new AppMasterDetailPageMasterMenuItem { Id = 2, Title = "Engineering" },
                    new AppMasterDetailPageMasterMenuItem { Id = 3, Title = "Math" },
                    new AppMasterDetailPageMasterMenuItem { Id = 4, Title = "Science" },
                    new AppMasterDetailPageMasterMenuItem { Id = 5, Title = "Sports" },
                    new AppMasterDetailPageMasterMenuItem { Id = 6, Title = "Tech" },
                    new AppMasterDetailPageMasterMenuItem { Id = 7, Title = "About", TargetType = typeof(AboutPage)}
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