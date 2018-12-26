using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Model;

namespace WinGuard.Domain.Interface
{

    public interface IClientAuthService
    {
        Task Login(string clientIdentifier, string clientName);
        Task LogOut(string clientIdentifier);
        Task<IEnumerable<ClientSession>> GetSessions();

    }

}
