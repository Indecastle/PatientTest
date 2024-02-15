using System.Text.Json;
using System.Text.Json.Serialization;
using BFP.App.Core.Models.Types;

namespace PatientTest
{
    public class StringConvertableJsonConverterFactory : JsonConverterFactory
    {
        private static readonly Type OPEN_GENERIC_MARKER_TYPE = typeof(ISingleValueObject<>);
        private static readonly Type OPEN_GENERIC_CONVERTER_TYPE = typeof(StringConvertableJsonConverter<,>);

        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert
                .GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == OPEN_GENERIC_MARKER_TYPE);
        }

        public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            var singleValueObjectInterface = typeToConvert.GetInterfaces().First(i =>
                i.IsGenericType && i.GetGenericTypeDefinition() == OPEN_GENERIC_MARKER_TYPE);
            var singleValueType = singleValueObjectInterface.GetGenericArguments().Single();
            var converterType = OPEN_GENERIC_CONVERTER_TYPE.MakeGenericType(typeToConvert, singleValueType);

            return Activator.CreateInstance(converterType) as JsonConverter;
        }
    }

    internal class StringConvertableJsonConverter<T, TValue> : JsonConverter<T>
        where T : class, ISingleValueObject<TValue>
    {
        private static readonly Type VALUE_TYPE = typeof(TValue);

        public override T? Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
            {
                return null;
            }

            var value = JsonSerializer.Deserialize(ref reader, VALUE_TYPE, options);
            return Activator.CreateInstance(typeToConvert, value) as T;
        }

        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value.Convert(), options);
        }
    }
}