using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;

using example.Models;
using example.Views;

namespace example.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        public ObservableCollection<Note> Items { get; set; }
        public Command LoadItemsCommand { get; set; }

        public ItemsViewModel()
        {
            Title = "Notes";
            Items = new ObservableCollection<Note>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());


            // triggered, when a new note is saved
            MessagingCenter.Subscribe<NewItemPage, Note>(this, "AddItem", async (obj, note) =>
            {
                var newNote = note as Note;
                Items.Add(newNote);
                await DataStore.AddItemAsync(newNote);
            });

            // triggered, when a note is deleted
            MessagingCenter.Subscribe<ItemsPage, Note>(this, "DeleteItem", async (obj, note) =>
            {
               var newNote = note as Note;
               Items.Remove(newNote);
               await DataStore.DeleteItemAsync(newNote.Id);
            });
        }

        // Get data from Mock-Database
        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                var notes = await DataStore.GetItemsAsync(true);
                foreach (var note in notes)
                {
                    Items.Add(note);
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