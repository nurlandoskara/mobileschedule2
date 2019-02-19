using System;
using MobileSchedule2.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MobileSchedule2.Services
{
    public class DataStore<T> : IDataStore<T> where T:BaseDbObject, new()
    {
        private List<T> _items;

        public DataStore()
        {
            _items = new List<T>();
        }
        
        public async Task<IEnumerable<T>> GetItemsAsync(string api, bool forceRefresh = false)
        {
            if(!forceRefresh) return _items;
            List<T> itemsAsync;
            if (App.IsOnline)
            {
                var json = await GetData(api);
                var items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(json));
                itemsAsync = items.ToList();
                foreach (var item in itemsAsync)
                {
                    item.ServerId = item.Id;
                    SaveLocal(item);
                }
            }
            else
            {
                itemsAsync = await App.DbConnection.Table<T>().ToListAsync();
            }

            _items = itemsAsync;
            return _items;
        }

        public async Task<IEnumerable<T>> GetItemsAsync(string api, int id, bool isForTeacher = false, bool forceRefresh = false)
        {
            if (!forceRefresh) return _items;
            List<T> itemsAsync;
            if (App.IsOnline)
            {
                var json = await GetData(api);
                var items = await Task.Run(() => JsonConvert.DeserializeObject<IEnumerable<T>>(json));
                itemsAsync = items.ToList();
                foreach (var item in itemsAsync)
                {
                    item.ServerId = item.Id;
                    item.GroupOrTeacherId = id;
                    item.IsForTeacher = isForTeacher;
                    SaveLocal(item);
                }
            }
            else
            {
                itemsAsync = await App.DbConnection.Table<T>().Where(x => x.GroupOrTeacherId == id && x.IsForTeacher == isForTeacher).ToListAsync();
            }

            _items = itemsAsync;
            return _items;
        }

        private static async Task<string> GetData(string api)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"{App.BackendUrl}/");
                var json = await client.GetStringAsync(api);
                return json;
            }
        }

        private static async void SaveLocal(T item)
        {
            var dbItem = await App.DbConnection.Table<T>().FirstOrDefaultAsync(x => x.ServerId == item.Id);
            if (dbItem != null)
            {
                await App.DbConnection.UpdateAsync(item);
            }
            else
            {
                await App.DbConnection.InsertAsync(item);
            }
        }
    }
}