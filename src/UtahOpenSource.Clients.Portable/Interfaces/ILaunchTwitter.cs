using System;

namespace UtahOpenSource.Clients.Portable
{
    public interface ILaunchTwitter
    {
        bool OpenUserName(string username);
        bool OpenStatus(string statusId);
    }
}

