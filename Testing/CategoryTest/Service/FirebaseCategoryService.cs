using CategoryTest.Model;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoryTest.Service
{
    public class FirebaseCategoryService : ICategorieService
    {
        private const string BaseUrl = "https://pcarchitectparts-default-rtdb.europe-west1.firebasedatabase.app/";

        private readonly ChildQuery _query;

        public FirebaseCategoryService()
        {
            string path = "cpu";
            _query = new FirebaseClient(BaseUrl).Child(path);
        }

        public async Task<List<Category>?> GetAll()
        {
            try
            {
                var firebaseObjects = await _query.OnceAsync<Category>();

                return firebaseObjects.Select(x => x.Object).ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
