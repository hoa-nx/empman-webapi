using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace EmpMan.Common
{
    public static class StringCipher
    {
        //https://stackoverflow.com/questions/10168240/encrypting-decrypting-a-string-in-c-sharp
        // This constant is used to determine the keysize of the encryption algorithm in bits.
        // We divide this by 8 within the code below to get the equivalent number of bytes.
        private const int Keysize = 256;

        // This constant determines the number of iterations for the password bytes generation function.
        private const int DerivationIterations = 1000;

        public static string Encrypt(string plainText, string passPhrase)
        {
            if (string.IsNullOrEmpty(plainText)) return "";
            // Salt and IV is randomly generated each time, but is preprended to encrypted cipher text
            // so that the same Salt and IV values can be used when decrypting.  
            var saltStringBytes = Generate256BitsOfRandomEntropy();
            var ivStringBytes = Generate256BitsOfRandomEntropy();
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var encryptor = symmetricKey.CreateEncryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                            {
                                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                                cryptoStream.FlushFinalBlock();
                                // Create the final bytes as a concatenation of the random salt bytes, the random iv bytes and the cipher bytes.
                                var cipherTextBytes = saltStringBytes;
                                cipherTextBytes = cipherTextBytes.Concat(ivStringBytes).ToArray();
                                cipherTextBytes = cipherTextBytes.Concat(memoryStream.ToArray()).ToArray();
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Convert.ToBase64String(cipherTextBytes);
                            }
                        }
                    }
                }
            }
        }

        public static string Decrypt(string cipherText, string passPhrase)
        {
            if (string.IsNullOrEmpty(cipherText)) return "";
            // Get the complete stream of bytes that represent:
            // [32 bytes of Salt] + [32 bytes of IV] + [n bytes of CipherText]
            var cipherTextBytesWithSaltAndIv = Convert.FromBase64String(cipherText);
            // Get the saltbytes by extracting the first 32 bytes from the supplied cipherText bytes.
            var saltStringBytes = cipherTextBytesWithSaltAndIv.Take(Keysize / 8).ToArray();
            // Get the IV bytes by extracting the next 32 bytes from the supplied cipherText bytes.
            var ivStringBytes = cipherTextBytesWithSaltAndIv.Skip(Keysize / 8).Take(Keysize / 8).ToArray();
            // Get the actual cipher text bytes by removing the first 64 bytes from the cipherText string.
            var cipherTextBytes = cipherTextBytesWithSaltAndIv.Skip((Keysize / 8) * 2).Take(cipherTextBytesWithSaltAndIv.Length - ((Keysize / 8) * 2)).ToArray();

            using (var password = new Rfc2898DeriveBytes(passPhrase, saltStringBytes, DerivationIterations))
            {
                var keyBytes = password.GetBytes(Keysize / 8);
                using (var symmetricKey = new RijndaelManaged())
                {
                    symmetricKey.BlockSize = 256;
                    symmetricKey.Mode = CipherMode.CBC;
                    symmetricKey.Padding = PaddingMode.PKCS7;
                    using (var decryptor = symmetricKey.CreateDecryptor(keyBytes, ivStringBytes))
                    {
                        using (var memoryStream = new MemoryStream(cipherTextBytes))
                        {
                            using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                            {
                                var plainTextBytes = new byte[cipherTextBytes.Length];
                                var decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                                memoryStream.Close();
                                cryptoStream.Close();
                                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                            }
                        }
                    }
                }
            }
        }

        private static byte[] Generate256BitsOfRandomEntropy()
        {
            var randomBytes = new byte[32]; // 32 Bytes will give us 256 bits.
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                // Fill the array with cryptographically secure random bytes.
                rngCsp.GetBytes(randomBytes);
            }
            return randomBytes;
        }

        public static byte[] AESEncryptBytes(byte[] clearBytes, byte[] passBytes, byte[] saltBytes)
        {
            byte[] encryptedBytes = null;

            // create a key from the password and salt, use 32K iterations – see note
            var key = new Rfc2898DeriveBytes(passBytes, saltBytes, 32768);

            // create an AES object
            using (Aes aes = new AesManaged())
            {
                // set the key size to 256
                aes.KeySize = 256;
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateEncryptor(),CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }
        public static byte[] AESDecryptBytes(byte[] cryptBytes, byte[] passBytes, byte[] saltBytes)
        {
            byte[] clearBytes = null;

            // create a key from the password and salt, use 32K iterations
            var key = new Rfc2898DeriveBytes(passBytes, saltBytes, 32768);

            using (Aes aes = new AesManaged())
            {
                // set the key size to 256
                aes.KeySize = 256;
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cryptBytes, 0, cryptBytes.Length);
                        cs.Close();
                    }
                    clearBytes = ms.ToArray();
                }
            }
            return clearBytes;
        }

        public static string AESEncryptString(string clearText, string passText, string saltText)
        {
            byte[] clearBytes = Encoding.UTF8.GetBytes(clearText);
            byte[] passBytes = Encoding.UTF8.GetBytes(passText);
            byte[] saltBytes = Encoding.UTF8.GetBytes(saltText);

            return Convert.ToBase64String(AESEncryptBytes(clearBytes, passBytes, saltBytes));
        }

        public static string AesDecryptString(string cryptText, string passText, string saltText)
        {
            byte[] cryptBytes = Convert.FromBase64String(cryptText);
            byte[] passBytes = Encoding.UTF8.GetBytes(passText);
            byte[] saltBytes = Encoding.UTF8.GetBytes(saltText);

            return Encoding.UTF8.GetString(AESDecryptBytes(cryptBytes, passBytes, saltBytes));
        }
    }
}
