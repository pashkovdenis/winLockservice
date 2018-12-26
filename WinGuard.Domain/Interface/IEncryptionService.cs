using System;
using System.Collections.Generic;
using System.Text;

namespace WinGuard.Domain.Interface
{
    public interface IEncryptionService
    {

        string DecryptMessage(string message);

        string EncryptMessage(string message);


    }
}
