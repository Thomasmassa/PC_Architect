using PcArchitect.Model;
using SQLite;

/*

De LocalDatabase klasse beheert de lokale SQLite-database.

De constructor initialiseert de SQLiteAsyncConnection, bepaalt het pad naar de database, en maakt een tabel voor het SavedBuild model als deze nog niet bestaat.
De database wordt opgeslagen in de lokale applicatiedata van de gebruiker.

GetItemsAsync haalt asynchroon alle SavedBuild items op uit de database.
SaveItemAsync voegt asynchroon een nieuw SavedBuild item toe aan de database en retourneert het aantal beïnvloede rijen.
DeleteItemAsync verwijdert asynchroon een SavedBuild item uit de database en retourneert het aantal beïnvloede rijen.
UpdateItemAsync werkt asynchroon een bestaand SavedBuild item bij in de database en retourneert het aantal beïnvloede rijen.

Het gebruik van asynchrone methoden zorgt voor een betere responsiviteit van de applicatie, omdat de UI thread niet wordt geblokkeerd tijdens database operaties.

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

        public Task SaveItemAsync(SavedBuild savedBuild)
        {
            return _database.InsertAsync(savedBuild);
        }

        public Task DeleteItemAsync(SavedBuild savedBuild)
        {
            return _database.DeleteAsync(savedBuild);
        }

        public Task UpdateItemAsync(SavedBuild savedBuild)
        {
            return _database.UpdateAsync(savedBuild);
        }
    }
}