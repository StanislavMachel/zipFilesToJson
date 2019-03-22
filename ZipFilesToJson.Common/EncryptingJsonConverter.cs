using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ZipFilesToJson.Common
{
    public class EncryptingJsonConverter : JsonConverter
    {

        private readonly IEncrypter _encrypter;

        public EncryptingJsonConverter(IEncrypter encrypter)
        {
            _encrypter = encrypter;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var stringValue = (string)value;
            if (string.IsNullOrEmpty(stringValue))
            {
                writer.WriteNull();
                return;
            }

            writer.WriteValue(_encrypter.Encrypt(stringValue));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            if (string.IsNullOrEmpty(value))
            {
                return reader.Value;
            }

            var byteArray = Convert.FromBase64String(value);

            return _encrypter.Decrypt(byteArray);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
