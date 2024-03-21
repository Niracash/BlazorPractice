using System.Security.Cryptography;

namespace BlazorPractice.Codes
{
    public class AsymetricHandler
    {
        private string? _privateKey;
        public string? _publicKey;

        //constructor
        public AsymetricHandler()
        {
            using (RSA rsa = RSA.Create())
            {
                RSAParameters privatKeyParameter = rsa.ExportParameters(true);
                _privateKey = rsa.ToXmlString(true);

                RSAParameters publicKeyParameter = rsa.ExportParameters(false);
                _privateKey = rsa.ToXmlString(false);
            }
        }
        public string EncryptAsymetric(string textToEncrypt) =>
            Encrypter.Encrypt(textToEncrypt, _publicKey);


        public string DecryptAsymetric(string textToDecrypt)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(_privateKey!);

                byte[] byteArrayTextToDecrypt = Convert.FromBase64String(textToDecrypt);
                byte[] decryptTextAsByteArray = rsa.Decrypt(byteArrayTextToDecrypt, true);

                string decryptedDataAsString = System.Text.Encoding.UTF8.GetString(decryptTextAsByteArray);
                return decryptedDataAsString;
            }
        }

    }
}
