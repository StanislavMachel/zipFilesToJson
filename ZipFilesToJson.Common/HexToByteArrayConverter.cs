using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ZipFilesToJson.Common
{
    public static class HexToByteArrayConverter
    {
        public static byte[] Convert(string hex)
        {
            if (string.IsNullOrEmpty(hex))
            {
                throw new ArgumentException("AppConfiguration.AesKey must be specified");
            }

            if (hex.Length % 2 != 0)
            {
                throw new ArgumentException(String.Format(CultureInfo.InvariantCulture,
                    "The binary key cannot have an odd number of digits: {0}", hex));
            }

            byte[] hexAsBytes = new byte[hex.Length / 2];
            for (int index = 0; index < hexAsBytes.Length; index++)
            {
                string byteValue = hex.Substring(index * 2, 2);
                hexAsBytes[index] = byte.Parse(byteValue, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            }

            return hexAsBytes;
        }
    }
}
