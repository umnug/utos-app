using System;
using UtahOpenSource.DataObjects;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UtahOpenSource.DataStore.Abstractions
{
    public interface ISessionStore : IBaseStore<Session>
    {
        Task<IEnumerable<Session>> GetSpeakerSessionsAsync(string speakerId);
        Task<IEnumerable<Session>> GetNextSessions();
        Task<Session> GetAppIndexSession (string id);
    }
}

