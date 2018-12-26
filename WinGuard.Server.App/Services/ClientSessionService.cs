using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;

namespace WinGuard.Server.App.Services
{

    public class ClientSessionService : IClientAuthService
    {
        private readonly ISessionStore _sessionStore;
        private readonly IMessageBus _messageBus;

        public ClientSessionService(ISessionStore sessionStore, 
            IMessageBus messageBus)
        {
            _sessionStore = sessionStore;
            _messageBus = messageBus;
        }

        public Task<IEnumerable<ClientSession>> GetSessions() => _sessionStore.GetSessions();

        public async Task Login(string clientIdentifier, string clientName) => await _sessionStore.CreateIFNotExists(clientIdentifier, clientName);
         
        public async Task LogOut(string clientIdentifier)
        {
            await _messageBus.Publish(new Message<ClientCommand>(new ClientCommand(Domain.Enumaretions.ClientCommandType.LOGOUT, clientIdentifier)));
            await _sessionStore.CloseSession(clientIdentifier);
        }


    }

}
