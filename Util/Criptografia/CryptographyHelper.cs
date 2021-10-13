using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Util.Criptografia
{
    public static class CryptographyHelper
    {
        public const string KeyString = "DE602AC85C20081F855BA9FDC59FA099";

        /// <summary>
        /// CRIPTOGRAFA A STRING PASSADA PARA A CRYPTOGRAFIA SHA256
        /// </summary>
        /// <param name="text"></param>
        /// <param name="keyString"></param>
        /// <returns></returns>
        public static string EncryptString(string text, string keyString = KeyString)
        {
            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var encryptor = aesAlg.CreateEncryptor(key, aesAlg.IV))
                {
                    using (var msEncrypt = new MemoryStream())
                    {
                        using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                        using (var swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(text);
                        }

                        var iv = aesAlg.IV;

                        var decryptedContent = msEncrypt.ToArray();

                        var result = new byte[iv.Length + decryptedContent.Length];

                        Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                        Buffer.BlockCopy(decryptedContent, 0, result, iv.Length, decryptedContent.Length);
                        var retorn = Convert.ToBase64String(result).Replace("=", "KIZ43").Replace("/", "Y1Z42").Replace("+", "OFLES");
                        return retorn;
                    }
                }
            }
        }

        public static object DecryptString(object token, string keyString = KeyString)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// DESENCRIPTOGRAFA A STRING PASSADA PARA A CRYPTOGRAFIA SHA256
        /// </summary>
        /// <param name="token"></param>
        /// <param name="keyString"></param>
        /// <returns></returns>
        public static string DecryptString(string cipherText, string keyString = KeyString)
        {
            cipherText = cipherText.Replace("KIZ43", "=").Replace("Y1Z42", "/").Replace("OFLES", "+");
            var fullCipher = Convert.FromBase64String(cipherText);

            var iv = new byte[16];
            //var cipher = new byte[16];
            var cipher = new byte[fullCipher.Length - iv.Length];


            Buffer.BlockCopy(fullCipher, 0, iv, 0, iv.Length);
            //Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, iv.Length);
            Buffer.BlockCopy(fullCipher, iv.Length, cipher, 0, fullCipher.Length - iv.Length);

            var key = Encoding.UTF8.GetBytes(keyString);

            using (var aesAlg = Aes.Create())
            {
                using (var decryptor = aesAlg.CreateDecryptor(key, iv))
                {
                    string result;
                    using (var msDecrypt = new MemoryStream(cipher))
                    {
                        using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                        {
                            using (var srDecrypt = new StreamReader(csDecrypt))
                            {
                                result = srDecrypt.ReadToEnd();
                            }
                        }
                    }
                    return result;
                }
            }
        }


        public static string GenerateHash(string text)
        {
            SHA256 crypto = SHA256.Create();
            var key = Encoding.UTF8.GetBytes(text);

            var hash = crypto.ComputeHash(key);

            return Encoding.UTF8.GetString(hash);
        }

        public static bool IsBase64(this string base64String)
        {
            if (base64String == null || base64String.Length == 0 || base64String.Length % 4 != 0
               || base64String.Contains(" ") || base64String.Contains("\t") || base64String.Contains("\r") || base64String.Contains("\n"))
                return false;


            Convert.FromBase64String(base64String);
            return true;


        }
    }
}
