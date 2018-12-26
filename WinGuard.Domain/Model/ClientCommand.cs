using System;
using System.Collections.Generic;
using System.Text;
using WinGuard.Domain.Enumaretions;

namespace WinGuard.Domain.Model
{
    public  class ClientCommand
    {
        public ClientCommand()
        {

        }
        public ClientCommand(ClientCommandType type, string indetifier)
        {
            Type = type;
            ClientIdentifier = indetifier;
        }

        public ClientCommandType Type { get; set; }

        public string ClientIdentifier { get; set; }

    }
}
