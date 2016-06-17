using System;
using System.Threading.Tasks;
using UtahOpenSource.DataObjects;
using UtahOpenSource.DataStore.Abstractions;

using Xamarin.Forms;
using System.Linq;
using UtahOpenSource.DataStore.Azure;

namespace UtahOpenSource.DataStore.Azure
{
    public class FavoriteStore : BaseStore<Favorite>, IFavoriteStore
    {
        public async  Task<bool> IsFavorite(string sessionId)
        {
            await InitializeStore().ConfigureAwait (false);
            var items = await Table.Where(s => s.SessionId == sessionId).ToListAsync().ConfigureAwait (false);
            return items.Count > 0;
        }

        public Task DropFavorites()
        {
            return Task.FromResult(true);
        }

        public override string Identifier => "Favorite";
    }
}

