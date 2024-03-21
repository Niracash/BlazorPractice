using System.Security.Cryptography;

namespace BlazorPractice.Codes
{
    public class Encrypter
    {
        public static string Encrypt(string textToEncrypt, string publicKey)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);

                byte[] byteArrayTextToEncrypt = System.Text.Encoding.UTF8.GetBytes(textToEncrypt);
                byte[] encryptTextAsByteArray = rsa.Encrypt(byteArrayTextToEncrypt, true);
                string encryptedDataAsString = Convert.ToBase64String(encryptTextAsByteArray);
                return encryptedDataAsString;

            }
        }
    }
}
