using System;
using Newtonsoft.Json;

namespace ZipFilesToJson.Common
{
    public class DecryptingJsonConverter : JsonConverter
    {
        private readonly IEncrypter _encrypter;

        public DecryptingJsonConverter(IEncrypter encrypter)
        {
            _encrypter = encrypter;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var byteArray16 = Convert.FromBase64String((string)value);
            writer.WriteValue(_encrypter.Decrypt(byteArray16));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var value = reader.Value as string;
            if (string.IsNullOrEmpty(value))
            {
                return reader.Value;
            }

            return _encrypter.Encrypt(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string);
        }
    }
}
