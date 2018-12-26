using System;
using System.Collections.Generic;
using System.Text;

namespace WinGuard.Domain.Model
{
    public class Message<T> where T : ClientCommand
    {
     
        public bool Processed { get; set; } = false;

        public string Id { get; set; }

        public DateTime Created { get; set; }

        public T Command { get ; private set; }

        public Message(T command)
        {
            this.Command = command;
            Id = Guid.NewGuid().ToString();
        } 

    }
}
