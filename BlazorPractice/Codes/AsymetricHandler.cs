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
            if (!File.Exists("privateKey.pem"))
            {

                using (RSA rsa = RSA.Create())
                {
                    RSAParameters privatKeyParameter = rsa.ExportParameters(true);
                    _privateKey = rsa.ToXmlString(true);

                    RSAParameters publicKeyParameter = rsa.ExportParameters(false);
                    _publicKey = rsa.ToXmlString(false);

                    File.WriteAllText("privateKey", _privateKey);
                    File.WriteAllText("publicKey", _publicKey);
                }
            }
            else
            {
               _privateKey = File.ReadAllText("privateKey.pem");
                _publicKey = File.ReadAllText("publicKey.pem");

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
