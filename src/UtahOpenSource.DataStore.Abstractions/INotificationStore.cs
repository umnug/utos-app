using System;
using UtahOpenSource.DataObjects;
using System.Threading.Tasks;

namespace UtahOpenSource.DataStore.Abstractions
{
    public interface INotificationStore : IBaseStore<Notification>
    {
        Task<Notification> GetLatestNotification();
    }
}

