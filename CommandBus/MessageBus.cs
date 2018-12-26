using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;

namespace CommandBus
{
    public class MessageBus : IMessageBus
    {
        private HashSet<Message<ClientCommand>> _messages;

        public MessageBus()
        {
            _messages = new HashSet<Message<ClientCommand>>();
        }

        public Task<IEnumerable<Message<ClientCommand>>> GetUnreadMessages()
        {
            return Task.FromResult<IEnumerable<Message<ClientCommand>>>(_messages.ToList());
        }

        public Task Publish(Message<ClientCommand> message)
        {
           return Task.FromResult(_messages.Add(message)); 
        }

        public void Read(ref Message<ClientCommand> message)
        {
            _messages.Remove(message); 
        }
    }
}
