using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;

namespace WinGuardServer.Controllers
{
    [Route("api/[controller]")]
    [Produces("Application/Json")]
    [ApiController]
    public class CommandController : ControllerBase
    {
        private readonly IMessageBus _messageBus;


        public CommandController(IMessageBus messageBus)
        {
            _messageBus = messageBus;

        } 

        [Route("read")]
        [HttpGet]
        public async Task<IEnumerable<Message<ClientCommand>>> GetMessages()
        {
            return await _messageBus.GetUnreadMessages();
        } 
        
        [Route("confirm")]
        [HttpGet]
        public async Task ReadMessage(string id)
        {
            var messages = await _messageBus.GetUnreadMessages();
            var message = messages.FirstOrDefault(m => m.Id == id);
            if (message != null)
                _messageBus.Read(ref message);
        }

    }
}