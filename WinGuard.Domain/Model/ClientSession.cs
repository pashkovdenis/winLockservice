using System;
using System.Collections.Generic;
using System.Text;

namespace WinGuard.Domain.Model
{
    public class ClientSession
    {
        public string ClientIdentifier { get; set; }

        public string UserName { get; set; }

        public DateTime LastPing { get; set; } 

    }
}
