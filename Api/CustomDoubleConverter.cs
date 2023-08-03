using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Api
{
	// This was implemented because in the tests, I was getting errors like: 123.0 not equal to 123.000
	public class CustomDecimalConverter : JsonConverter<decimal>
	{
		public override decimal ReadJson(JsonReader reader, Type objectType, decimal existingValue, bool hasExistingValue, JsonSerializer serializer)
		{
			JToken token = JToken.Load(reader);

			if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
			{
				return token.Value<decimal>();
			}

			return 0.0m;
		}

		public override void WriteJson(JsonWriter writer, decimal value, JsonSerializer serializer)
		{
			writer.WriteValue(value);
		}
	}
}
