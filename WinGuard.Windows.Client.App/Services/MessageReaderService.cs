using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WinGuard.Domain.Model;
using WinGuard.Windows.Client.App.Interface;

namespace WinGuard.Windows.Client.App.Services
{
    public class MessageReaderService : IMessageReader
    {
        private readonly string _url; 
        public MessageReaderService(string url )
        {
            _url = url;
        }

        public async Task Confirm(Message<ClientCommand> message)
        {
            using (var client = new WebClient())
            {
                await client.DownloadStringTaskAsync($"{_url}/api/command/confirm?id={message.Id}");
            }
        }

        public async Task<IEnumerable<Message<ClientCommand>>> GetAllMessages()
        {
             using (var client = new WebClient())
            {
                var data = await client.DownloadStringTaskAsync($"{_url}/api/command/read");
                return JsonConvert.DeserializeObject<IEnumerable<Message<ClientCommand>>>(data); 
            } 
        }
        
    }
}
