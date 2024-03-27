using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using PC_Architect.Model;

namespace PC_Architect.Services
{
    public class FirebasePartsService : IPartsService
    {
        private const string BaseUrl = "https://pcarchitectparts-default-rtdb.europe-west1.firebasedatabase.app/";

        private readonly ChildQuery query;

        public FirebasePartsService()
        {
            string path = "parts";
            query = new FirebaseClient(BaseUrl).Child(path);
        }

        public async Task<List<Cpu>?> GetCPUs()
        {
            try
            {
                var firebaseObjects = await query.OnceAsync<Cpu>();
                return firebaseObjects.Select(firebaseObject => firebaseObject.Object).ToList();
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
