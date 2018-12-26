using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Model;

namespace WinGuard.Windows.Client.App.Interface
{
    public interface IMessageReader
    {

        Task<IEnumerable<Message<ClientCommand>>> GetAllMessages();

        Task Confirm(Message<ClientCommand> message);  


    }
}
