using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoffeeBeans.Services
{
    public interface IDataStore<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> AddItemAsyncOrder(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        // orders page
        Task<IEnumerable<T>> GetItemsAsyncOrder(bool forceRefresh = false);
        Task<IEnumerable<T>> GetItemsAsyncTransit(bool forceRefresh = false);
        Task<IEnumerable<T>> GetItemsAsyncHistory(bool forceRefresh = false);

        Task<bool> RandomizeItems();
    }
}
