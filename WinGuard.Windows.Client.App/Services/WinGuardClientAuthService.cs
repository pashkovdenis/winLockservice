using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;
using System.Net;

namespace WinGuard.Windows.Client.App.Services
{
    public class WinGuardClientAuthService : IClientAuthService
    {
        private readonly string _serverUrl;
        public WinGuardClientAuthService(string serverUrl)
        {
            _serverUrl = serverUrl; 
        }
        public Task<IEnumerable<ClientSession>> GetSessions()
        {
            throw new NotImplementedException();  
        }
        public async Task Login(string clientIdentifier, string clientName)
        { 
            using (var client = new WebClient())
            {
                await client.DownloadStringTaskAsync($"{_serverUrl}/api/client/login?clientId={clientIdentifier}&userName={clientName}");
            }
        }
        public async Task LogOut(string clientIdentifier)
        {
            using (var client = new WebClient())
            {
                await client.DownloadStringTaskAsync($"{_serverUrl}/api/client/logout?clientId={clientIdentifier}");
            }
        }

    }
}
