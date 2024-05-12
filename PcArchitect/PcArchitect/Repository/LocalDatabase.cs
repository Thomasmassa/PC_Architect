using PcArchitect.Model;
using SQLite;

/*
De LocalDatabase klasse beheert de lokale SQLite-database.

De constructor initialiseert de SQLiteAsyncConnection en maakt een tabel voor het SavedBuild model.
De database wordt opgeslagen in de lokale applicatiedata van de gebruiker.

GetItemsAsync haalt alle SavedBuild items op uit de database.
SaveItemAsync voegt een nieuw SavedBuild item toe aan de database.
DeleteItemAsync verwijdert een SavedBuild item uit de database.
UpdateItemAsync werkt een bestaand SavedBuild item bij in de database.

Elke methode is asynchroon en retourneert een Task of Task<int>, waarbij de int het aantal beïnvloede rijen aangeeft.
*/

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