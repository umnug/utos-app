using UtahOpenSource.DataStore.Abstractions;
using UtahOpenSource.DataObjects;
using UtahOpenSource.DataStore.Azure;

namespace UtahOpenSource.DataStore.Azure
{
    public class SpeakerStore : BaseStore<Speaker>, ISpeakerStore
    {
        public override string Identifier => "Speaker";
    }
}

