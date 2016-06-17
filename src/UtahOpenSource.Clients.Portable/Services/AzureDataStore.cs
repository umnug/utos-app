using System;
using UtahOpenSource.Portable.Services;
using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;
using Microsoft.WindowsAzure.MobileServices.Sync;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.MobileServices.SQLiteStore;
using UtahOpenSource.Portable.Interfaces;


[assembly: Dependency(typeof(AzureDataStore))]
namespace UtahOpenSource.Portable.Services
{
    public class AzureDataStore : IDataStore
    {
        public MobileServiceClient MobileService { get; set; }

        //IMobileServiceSyncTable<Monkey> monkeyTable;
        bool initialized = false;

        public AzureDataStore()
        {
            MobileService = new MobileServiceClient(
                "https://utahopensource.azurewebsites.net");
        }

        #region IDataStore implementation

        public async Task InitializeStore()
        {
            initialized = true;
            const string path = "syncstore.db";
            var store = new MobileServiceSQLiteStore(path);
            //store.DefineTable<Monkey>();
            await MobileService.SyncContext.InitializeAsync(store, new MobileServiceSyncHandler());

            //monkeyTable = MobileService.GetSyncTable<Monkey>();
        }

        public UtahOpenSource.Portable.Model.Individual GetSpeaker(string name)
        {
            throw new NotImplementedException();
        }

        public Task<UtahOpenSource.Portable.Model.Session> GetSessionAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<UtahOpenSource.Portable.Model.Session>> GetSessionsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<UtahOpenSource.Portable.Model.Session>> GetSpeakerSessionsAsync(string speakerName)
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<UtahOpenSource.Portable.Model.Individual>> GetSpeakersAsync()
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<UtahOpenSource.Portable.Model.Individual>> GetSponsorsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<UtahOpenSource.Portable.Model.Individual>> GetExhibitorsAsync()
        {
            throw new NotImplementedException();
        }

        #endregion

        /*public async Task<IEnumerable<Monkey>> GetMonkeysAsync()
        {
            if (!initialized)
                await InitializeStore();

            await monkeyTable.PullAsync("allMonkeys", monkeyTable.CreateQuery());

            return await monkeyTable.OrderBy(x=>x.Name).ToEnumerableAsync();
        }*/
    }
}
