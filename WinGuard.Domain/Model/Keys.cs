using System;
using System.Collections.Generic;
using System.Text;

namespace WinGuard.Domain.Model
{
    /// <summary>
    /// TODO:  We should use secured storege for this or at least SecureString ...  
    /// for now just leave it like this ) 
    /// 
    /// </summary>
    public static class Keys
    { 
        public const string AesIV256 = @"1234123456785678";
        public const string AesKey256 = @"4566456678997899";
    }
}
