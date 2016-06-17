using System;
using UtahOpenSource.DataObjects;

namespace UtahOpenSource.DataStore.Abstractions
{
    public interface IEventStore : IBaseStore<FeaturedEvent>
    {
    }
}

