using System.Security.Cryptography;
using System.Text;

namespace D.Untils.DataUntils
{
    public static class AesEncryption
    {
        private static readonly string DefaultKey = "D.University2026SecretKey1234567"; // 32 bytes for AES-256

        public static string Encrypt(string plainText, string? key = null)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            var secretKey = key ?? DefaultKey;
            
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(secretKey.PadRight(32).Substring(0, 32));
            aes.GenerateIV();
            
            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            
            // Combine IV + encrypted data
            var result = new byte[aes.IV.Length + encryptedBytes.Length];
            Buffer.BlockCopy(aes.IV, 0, result, 0, aes.IV.Length);
            Buffer.BlockCopy(encryptedBytes, 0, result, aes.IV.Length, encryptedBytes.Length);
            
            return Convert.ToBase64String(result);
        }

        public static string Decrypt(string cipherText, string? key = null)
        {
            if (string.IsNullOrEmpty(cipherText))
                return cipherText;

            var secretKey = key ?? DefaultKey;
            
            var fullCipher = Convert.FromBase64String(cipherText);
            
            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(secretKey.PadRight(32).Substring(0, 32));
            
            // Extract IV from the beginning
            var iv = new byte[16];
            var cipherBytes = new byte[fullCipher.Length - 16];
            Buffer.BlockCopy(fullCipher, 0, iv, 0, 16);
            Buffer.BlockCopy(fullCipher, 16, cipherBytes, 0, cipherBytes.Length);
            
            aes.IV = iv;
            
            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            var decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);
            
            return Encoding.UTF8.GetString(decryptedBytes);
        }
    }
}
