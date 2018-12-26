using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Model;

namespace WinGuard.Domain.Interface
{
    public interface IMessageBus
    {
        Task Publish(Message<ClientCommand> message);

        Task<IEnumerable<Message<ClientCommand>>> GetUnreadMessages();

        void Read(ref Message<ClientCommand> message);

    }
}
