using System;
using UtahOpenSource.DataObjects;
using UtahOpenSource.DataStore.Abstractions;

namespace UtahOpenSource.DataStore.Azure
{
    public class MiniHacksStore : BaseStore<MiniHack>, IMiniHacksStore
    {
        public MiniHacksStore()
        {
        }

        public override string Identifier => "MiniHacks";
    }
}

