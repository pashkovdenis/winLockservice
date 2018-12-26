using System;
using System.Security.Cryptography;
using System.Text;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;

namespace EncryptionService
{
    public class EncryptDecryptService : IEncryptionService
    {
        private readonly AesCryptoServiceProvider cryptoServiceProvider;
        public EncryptDecryptService(string ivKey, string aesKEy)
        {
            cryptoServiceProvider = new AesCryptoServiceProvider();
            cryptoServiceProvider.BlockSize = 128;
            cryptoServiceProvider.KeySize = 256;
            cryptoServiceProvider.IV = Encoding.UTF8.GetBytes(ivKey);
            cryptoServiceProvider.Key = Encoding.UTF8.GetBytes(aesKEy);
            cryptoServiceProvider.Mode = CipherMode.CBC;
            cryptoServiceProvider.Padding = PaddingMode.PKCS7;
        } 
        public string DecryptMessage(string message64Encoded)
        {
            var bytes = Convert.FromBase64String(message64Encoded);
            using(var decriptor = cryptoServiceProvider.CreateDecryptor(cryptoServiceProvider.Key, cryptoServiceProvider.IV))
            {
                var result = decriptor.TransformFinalBlock(bytes, 0, bytes.Length);
                return Encoding.UTF8.GetString(result);
            }  
        }
        public string EncryptMessage(string message)
        {
            var bytes = Encoding.UTF8.GetBytes(message);
            using (var decriptor = cryptoServiceProvider.CreateEncryptor(cryptoServiceProvider.Key, cryptoServiceProvider.IV))
            {
                var result = decriptor.TransformFinalBlock(bytes, 0, bytes.Length);
                return Convert.ToBase64String(result);
            } 
        }
    }
}
