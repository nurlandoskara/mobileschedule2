using MobileSchedule2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileSchedule2.Services
{
    public class MockDataStore<T> : IDataStore<T> where T:BaseDbObject, new()
    {
        private readonly List<T> _items;

        public MockDataStore()
        {
            _items = new List<T>();
            var mockItems = new List<T>
            {
                new T { Id = 0, Title = "First item"},
                new T { Id = 1, Title = "Second item"},
                new T { Id = 2, Title = "Third item"},
                new T { Id = 3, Title = "Fourth item"},
                new T { Id = 4, Title = "Fifth item"},
                new T { Id = 5, Title = "Sixth item"},
            };

            foreach (var item in mockItems)
            {
                _items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(T item)
        {
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(T item)
        {
            var oldItem = _items.FirstOrDefault(arg => arg.Id == item.Id);
            _items.Remove(oldItem);
            _items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var oldItem = _items.FirstOrDefault(arg => arg.Id == id);
            _items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<T> GetItemAsync(int id)
        {
            return await Task.FromResult(_items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false, int groupId = 0)
        {
            return await Task.FromResult(_items);
        }
    }
}