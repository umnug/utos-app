using System;
using System.Threading.Tasks;

namespace UtahOpenSource.Clients.Portable
{
    public interface ISSOClient
    {
        Task<AccountResponse> LoginAsync(string username, string password);

        Task LogoutAsync();
    }
}

