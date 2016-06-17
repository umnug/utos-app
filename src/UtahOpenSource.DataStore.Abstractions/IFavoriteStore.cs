using System;
using System.Threading.Tasks;
using UtahOpenSource.DataObjects;
using System.Collections.Generic;

namespace UtahOpenSource.DataStore.Abstractions
{
    public interface IFavoriteStore : IBaseStore<Favorite>
    {
        Task<bool> IsFavorite(string sessionId);
        Task DropFavorites();
    }
}

