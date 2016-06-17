using UtahOpenSource.DataStore.Abstractions;
using UtahOpenSource.DataObjects;
using UtahOpenSource.DataStore.Azure;

namespace UtahOpenSource.DataStore.Azure
{
    public class SponsorStore : BaseStore<Sponsor>, ISponsorStore
    {
        public override string Identifier => "Sponsor";
    }
}

