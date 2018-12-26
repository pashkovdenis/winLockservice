using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Model;

namespace WinGuard.Domain.Interface
{
    public interface ISessionStore
    {
        Task CreateIFNotExists(string sessionId, string userName);

        Task CloseSession(string sessionId);

        Task<IEnumerable<ClientSession>> GetSessions(); 
    
         
    }
}
