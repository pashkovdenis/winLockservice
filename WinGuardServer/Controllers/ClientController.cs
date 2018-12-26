using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using WinGuard.Domain.Interface;

namespace WinGuardServer.Controllers
{
    [Route("api/[controller]")]
    [Produces("Application/Json")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientAuthService _service;
        private readonly IEncryptionService _encryptService;

        public ClientController(IClientAuthService service, IEncryptionService encryptService)
        {
            _service = service;
            _encryptService = encryptService;
        }

        [Route("login")]
        [HttpGet]
        public async Task<bool> Login(string clientId, string userName)
        { 
            await _service.Login( _encryptService.DecryptMessage( clientId ),
                _encryptService.DecryptMessage(userName)); 
            return true; 
        }

        [Route("logout")]
        [HttpGet]
        public async Task<bool> Logout(string clientId)
        {
            await _service.LogOut(clientId);
            return true;
        }
         
        [Route("version")]
        public object Version() => new { Version = 1 };
    }
}