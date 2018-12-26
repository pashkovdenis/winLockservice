using System;
using System.Text;
using WinGuard.Domain.Interface;
using WinGuard.Domain.Model;
using Xunit;

namespace TestEcription
{
    public class EncryptDecryptTests
    {
        private readonly IEncryptionService service; 

        public EncryptDecryptTests()
        {
            service = new EncryptionService.EncryptDecryptService(Keys.AesIV256, Keys.AesKey256);

        }


        [Fact]
        public void TestEcryptDecrypt()
        { 
            var example = "Hello World";
            var encrypted = service.EncryptMessage(example); 
            var decrypted = service.DecryptMessage(encrypted); 
            Assert.Equal(example, decrypted); 
        } 


    }
}
