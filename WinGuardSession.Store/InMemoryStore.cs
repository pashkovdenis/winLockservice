using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;

namespace WinGuardSession.Store
{
    public class InMemoryStore : ISessionStore
    {
        private readonly IMemoryCache _cache;
        private List<ClientSession> _sessions;  
        public InMemoryStore(IMemoryCache cache)
        { 
            _cache = cache;
            _sessions = _cache.GetOrCreate("sessions", sessions => new List<ClientSession>());
        }

        public Task CloseSession(string sessionId)
        {
            if (_sessions.Any(s => s.ClientIdentifier == sessionId.Trim()))
                _sessions.Remove(_sessions.First(s => s.ClientIdentifier == sessionId.Trim()));
            _cache.Set("sessions", _sessions);

            return Task.CompletedTask;
        }

        public  Task CreateIFNotExists(string sessionId, string userName)
        {
            if (!_sessions.Any(s => s.ClientIdentifier == sessionId))
                _sessions.Add(new ClientSession { ClientIdentifier = sessionId, UserName = userName });
            _cache.Set("sessions", _sessions);

            return Task.CompletedTask; 
        }

        public Task<IEnumerable<ClientSession>> GetSessions()
        {
            _sessions = _cache.GetOrCreate("sessions", sessions => new List<ClientSession>());
            return Task.FromResult<IEnumerable<ClientSession>>(_sessions); 
        }
    }
}
