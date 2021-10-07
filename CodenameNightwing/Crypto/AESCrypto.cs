using log4net;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CodenameNightwing.Crypto
{
    class AESCrypto
    {

        public static readonly ILog logger = LogManager.GetLogger(typeof(AESCrypto));

        public static string Encrypt(string raw , string password)
        {
            try
            {
                using (var csp = new AesCryptoServiceProvider())
                {
                    ICryptoTransform e = GetCryptoTransform(csp, true, password);
                    byte[] inputBuffer = Encoding.UTF8.GetBytes(raw);
                    byte[] output = e.TransformFinalBlock(inputBuffer, 0, inputBuffer.Length);
                    string encrypted = Convert.ToBase64String(output);
                    return encrypted;
                }
            }
            catch ( Exception e)
            {
                logger.Error("Error encriptando", e);
                return null;
            }

        }

        public static string Decrypt(string encrypted , string password)
        {
            try { 
                using (var csp = new AesCryptoServiceProvider())
                {
                    var d = GetCryptoTransform(csp, false, password);
                    byte[] output = Convert.FromBase64String(encrypted);
                    byte[] decryptedOutput = d.TransformFinalBlock(output, 0, output.Length);

                    string decypted = Encoding.UTF8.GetString(decryptedOutput);
                    return decypted;
                }
            }
            catch (Exception e)
            {
                logger.Error("Error descriptando", e);
                return null;
            }
        }

        private static ICryptoTransform GetCryptoTransform(AesCryptoServiceProvider csp, bool encrypting , string password)
        {
            csp.Mode = CipherMode.CBC;
            csp.Padding = PaddingMode.PKCS7;
            var salt = "S@1tS@lt";
            

            //a random Init. Vector. just for testing
            String iv = "e675f725e675f725";

            var spec = new Rfc2898DeriveBytes(Encoding.UTF8.GetBytes(password), Encoding.UTF8.GetBytes(salt), 65536);
            byte[] key = spec.GetBytes(16);


            csp.IV = Encoding.UTF8.GetBytes(iv);
            csp.Key = key;
            if (encrypting)
            {
                return csp.CreateEncryptor();
            }
            return csp.CreateDecryptor();
        }

    }
}
