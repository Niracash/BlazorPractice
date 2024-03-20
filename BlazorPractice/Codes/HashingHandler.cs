using System.Security.Cryptography;
using System.Text;

namespace BlazorPractice.Codes
{
    public class HashingHandler
    {
        public string MD5Hashing(string textToHash)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
            byte[] hashedValue = md5.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashedValue);
        }
        public string SHA2Hashing(string textToHash)
        {
            SHA256 sha = SHA256.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
            byte[] hashedValue = sha.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashedValue);
        }
        public string HMACHashing(string textToHash)
        {
            byte[] myKey = Encoding.ASCII.GetBytes("Something");
            byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);

            HMACSHA256 hmac = new HMACSHA256();
            hmac.Key = myKey;
            byte[] hashedValue = hmac.ComputeHash(inputBytes);
            return Convert.ToBase64String(hashedValue);
        }
        public string PBKDF2Hashing(string textToHash)
        {
            byte[] inputBytes = Encoding.ASCII.GetBytes(textToHash);
            byte[] salt = Encoding.ASCII.GetBytes("mytestsalt");

            var hashingAlgo = new HashAlgorithmName("SHA256");
            int itirations = 10;
            int outputLength = 32;

            byte[] hashedValue = Rfc2898DeriveBytes.Pbkdf2(inputBytes, salt, itirations, hashingAlgo, outputLength);
            return Convert.ToBase64String(hashedValue);
        }

        public string BCryptHashing(string textToHash)
        {
            // int workFactor = 10;
            // bool entropy = true;
            // return BCrypt.Net.BCrypt.HashPassword(textToHash, workFactor, entropy);

            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            bool entropy = true;
            var hashType = BCrypt.Net.HashType.SHA256;

            return BCrypt.Net.BCrypt.HashPassword(textToHash, salt, entropy, hashType);

        }

        public bool BCryptHashingValidation(string textToHash, string hashedValue)
        {
            //return BCrypt.Net.BCrypt.HashPassword(textToHash);
            // return BCrypt.Net.BCrypt.Verify(textToHash, hashedValue, true);

            return BCrypt.Net.BCrypt.Verify(textToHash, hashedValue, true, BCrypt.Net.HashType.SHA256);
        }

    }
}
