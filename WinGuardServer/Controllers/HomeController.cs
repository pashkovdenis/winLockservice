using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WinGuard.Domain.Interface;

namespace WinGuardServer.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClientAuthService _service; 

        public HomeController(IClientAuthService service)
        {
            _service = service;
        }
        
        public async Task<IActionResult> Index()
        {
            var sessions = await _service.GetSessions();
            return View(sessions);
        }
         
        public async Task<IActionResult> kick(string id)
        {
            await _service.LogOut(id);
            return RedirectToAction("index"); 
        }


    }
}