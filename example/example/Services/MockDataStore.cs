using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using example.Models;

namespace example.Services
{
    public class MockDataStore : IDataStore<Note>
    {
        readonly List<Note> notes;

        public MockDataStore()
        {
            notes = new List<Note>()
            {
                new Note { Id = Guid.NewGuid().ToString(), Text = "Das ist meine erste Notiz und dient zur Veranschaulichung. Das Designen macht mir Spaß", ImageUrl = "https://picsum.photos/50/50?nocache=" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() },
                new Note { Id = Guid.NewGuid().ToString(), Text = "Second item",  ImageUrl = "https://picsum.photos/50/50?nocache=" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString()
        },
                new Note { Id = Guid.NewGuid().ToString(), Text = "Sixth item", ImageUrl =  "https://picsum.photos/50/50?nocache=" + DateTimeOffset.UtcNow.ToUnixTimeMilliseconds().ToString() }
            };
        }

        public async Task<bool> AddItemAsync(Note note)
        {
            notes.Add(note);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Note note)
        {
            var oldItem = notes.Where((Note arg) => arg.Id == note.Id).FirstOrDefault();
            notes.Remove(oldItem);
            notes.Add(note);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = notes.Where((Note arg) => arg.Id == id).FirstOrDefault();
            notes.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Note> GetItemAsync(string id)
        {
            return await Task.FromResult(notes.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Note>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(notes);
        }
    }
}