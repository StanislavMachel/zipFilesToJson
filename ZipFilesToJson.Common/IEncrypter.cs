using System;
using System.Collections.Generic;
using System.Text;

namespace ZipFilesToJson.Common
{
    public interface IEncrypter
    {
        byte[] Encrypt(string plainText);
        string Decrypt(byte[] chipterText);
    }
}
