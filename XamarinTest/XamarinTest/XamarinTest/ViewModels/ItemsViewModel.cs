using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinTest.Helpers;
using XamarinTest.Models;
using XamarinTest.Views;

namespace XamarinTest.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Hobby> Items { get; set; }
        public Command LoadItemsCommand { get; set; }
        FirestoreHelper firestoreHelper = new FirestoreHelper();
        public ItemsViewModel()
        {
            Title = "Browse";
            Items = new ObservableCollection<Hobby>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Hobby>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Hobby;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        public ItemsViewModel(String pageTitle)
        {
            Title = pageTitle;
            Items = new ObservableCollection<Hobby>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());

            MessagingCenter.Subscribe<NewItemPage, Hobby>(this, "AddItem", async (obj, item) =>
            {
                var newItem = item as Hobby;
                Items.Add(newItem);
                await DataStore.AddItemAsync(newItem);
            });
        }

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var items = await firestoreHelper.GetHobbiesByParent(Title);
                foreach (var item in items)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async Task ExecuteLoadItemsCommand(List<Hobby> toLoad)
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                //var items = await DataStore.GetItemsAsync(true);
                foreach (var item in toLoad)
                {
                    Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}