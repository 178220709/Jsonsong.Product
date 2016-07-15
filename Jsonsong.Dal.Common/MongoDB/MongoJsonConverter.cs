using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using Newtonsoft.Json;
using static System.String;
using JsonToken = Newtonsoft.Json.JsonToken;

namespace Jsonsong.Dal.Common.MongoDb
{
    public class MongoJsonConverter : JsonConverter
    {
        public override void WriteJson(Newtonsoft.Json.JsonWriter writer, object value, JsonSerializer serializer)
        {
            if (value is ObjectId)
            {
                var objectId = (ObjectId) value;

                writer.WriteValue(objectId != ObjectId.Empty ? objectId.ToString() : String.Empty);
            }
            else
            {
                throw new Exception("Expected ObjectId value.");
            }
        }

        public override object ReadJson(Newtonsoft.Json.JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            if (reader.TokenType != JsonToken.String)
            {
                throw new Exception(
                    $"Unexpected token parsing ObjectId. Expected String, got {reader.TokenType}.");
            }

            var value = (string) reader.Value;
            return IsNullOrEmpty(value) ? ObjectId.Empty : new ObjectId(value);
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ObjectId);
        }
    }
}