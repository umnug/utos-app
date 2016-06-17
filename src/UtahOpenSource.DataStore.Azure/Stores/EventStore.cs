using System;
using UtahOpenSource.DataObjects;
using UtahOpenSource.DataStore.Abstractions;

namespace UtahOpenSource.DataStore.Azure
{
    public class EventStore : BaseStore<FeaturedEvent>, IEventStore
    {
        public override string Identifier => "FeaturedEvent";
        public EventStore()
        {
            
        }
    }
}

