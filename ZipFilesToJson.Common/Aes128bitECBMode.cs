using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace ZipFilesToJson.Common
{
    public class Aes128BitEcbMode : IEncrypter
    {
        private readonly IConfiguration _configuration;

        public Aes128BitEcbMode(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private AesManaged GetAesManaged()
        {
            var aesManaged = new AesManaged
            {
                Mode = CipherMode.ECB,
                KeySize = 128,
                BlockSize = 128
            };

            var hex = _configuration.GetSection("AppConfiguration")["AesKey"];

            aesManaged.Key = HexToByteArrayConverter.Convert(hex);
            return aesManaged;
        }

        public byte[] Encrypt(string plainText)
        {
            using (SymmetricAlgorithm symmetricAlgorithm = GetAesManaged())
            {
                ICryptoTransform encryptor =
                    symmetricAlgorithm.CreateEncryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream scEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(scEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }

                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        public string Decrypt(byte[] chipterText)
        {
            
            using (SymmetricAlgorithm symmetricAlgorithm = GetAesManaged())
            {
                ICryptoTransform decryptor =
                    symmetricAlgorithm.CreateDecryptor(symmetricAlgorithm.Key, symmetricAlgorithm.IV);
                using (MemoryStream msDecrypt = new MemoryStream(chipterText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}
