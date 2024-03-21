using Microsoft.AspNetCore.DataProtection;

namespace BlazorPractice.Codes
{
    public class SymetricHandler
    {
        private readonly IDataProtector _protector;
        public SymetricHandler(IDataProtectionProvider protector)
        {
            _protector = protector.CreateProtector("Key");
        }
        public string Encrypt(string texToEncrypt) =>
                        _protector.Protect(texToEncrypt);
        public string Decrypt(string texToDecrypt) =>
                _protector.Unprotect(texToDecrypt);
    }
}
