using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using UtahOpenSource.DataObjects;

using UtahOpenSource.DataStore.Azure;
using UtahOpenSource.DataStore.Abstractions;

namespace UtahOpenSource.DataStore.Azure
{
    public class FeedbackStore : BaseStore<Feedback>, IFeedbackStore
    {
        public async Task<bool> LeftFeedback(Session session)
        {
            await InitializeStore();
            var items = await Table.Where(s => s.SessionId == session.Id).ToListAsync().ConfigureAwait (false);
            return items.Count > 0;
        }

        public Task DropFeedback()
        {
            return Task.FromResult(true);
        }

     

        public override string Identifier => "Feedback";
         
    }
}

