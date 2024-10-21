using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ForecasterText.Objects.Addons {
    public sealed class EmojiSetConverter : JsonConverter<EmojiSet> {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, EmojiSet value, JsonSerializer serializer) {
            if (value.Count is 1) {
                writer.WriteValue(value.First());
            } else {
                // Start an array
                writer.WriteStartArray();
                
                foreach (uint id in value)
                    writer.WriteValue(id);
                
                // End the array
                writer.WriteEndArray();
            }
        }
        
        /// <inheritdoc />
        public override EmojiSet ReadJson(JsonReader reader, Type objectType, EmojiSet existingValue, bool hasExistingValue, JsonSerializer serializer) {
            if (reader.TokenType is JsonToken.Integer) {
                
                if (this.ReadUInt(reader) is uint id)
                    return id;
                
            } else if (reader.TokenType is JsonToken.StartArray) {
                List<uint> ids = new();
                
                while (reader.TokenType is not JsonToken.EndArray && reader.Read()) {
                    if (this.ReadUInt(reader) is uint id && id > 0)
                        ids.Add(id);
                }
                
                return new EmojiSet(ids);
            }
            
            return existingValue;
        }
        
        private uint ReadUInt(JsonReader reader) {
            if (reader.TokenType is JsonToken.Integer or JsonToken.Float) {
                object? val = reader.Value;
                
                if (val is long l)
                    return (uint) l;
                
                if (val is int i)
                    return (uint) i;
                
                if (val is float f)
                    return (uint) f;
            }
            
            return 0u;
        }
    }
}
