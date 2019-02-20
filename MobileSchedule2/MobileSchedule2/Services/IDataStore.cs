using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileSchedule2.Services
{
    public interface IDataStore<T>
    {
        Task<IEnumerable<T>> GetItemsAsync(string api, bool forceRefresh = false);
        Task<IEnumerable<T>> GetLessonsAsync(string api, int id, bool isForTeacher = false, bool forceRefresh = false);
    }
}
