using System;
using System.Threading.Tasks;
using UtahOpenSource.DataObjects;

namespace UtahOpenSource.DataStore.Abstractions
{
    public interface IFeedbackStore : IBaseStore<Feedback>
    {
        Task<bool> LeftFeedback(Session session);
        Task DropFeedback();
    }
}

