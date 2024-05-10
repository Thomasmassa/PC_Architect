using PcArchitect.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PcArchitect.Repository
{
    public class LocalDatabase
    {
        readonly SQLiteAsyncConnection _database;

        public LocalDatabase()
        {
            string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "mydatabase.db3");
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<SavedBuild>().Wait();
        }

        public Task<List<SavedBuild>> GetItemsAsync()
        {
            return _database.Table<SavedBuild>().ToListAsync();
        }

        public Task<int> SaveItemAsync(SavedBuild savedBuild)
        {
            return _database.InsertAsync(savedBuild);
        }

        public Task<int> DeleteItemAsync(SavedBuild savedBuild)
        {
            return _database.DeleteAsync(savedBuild);
        }

        public Task<int> UpdateItemAsync(SavedBuild savedBuild)
        {
            return _database.UpdateAsync(savedBuild);
        }
    }
}