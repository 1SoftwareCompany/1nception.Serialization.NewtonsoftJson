using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;

namespace One.Inception.Serialization.NewtonsoftJson
{
    internal sealed class MessageIdValueProvider : IValueProvider
    {
        public const string PropertyName = "messageId";

        private static readonly MessageIdValueProvider instance = new();

        public static JsonProperty CreateProperty(Type declaringType) => new()
        {
            PropertyName = PropertyName,
            PropertyType = typeof(string),
            DeclaringType = declaringType,
            ValueProvider = instance,
            Readable = true,
            Writable = true,
            NullValueHandling = NullValueHandling.Ignore,
            Order = int.MaxValue
        };

        public object GetValue(object target) => MessageIds.Get((IMessage)target);

        public void SetValue(object target, object value)
        {
            if (value is string id)
                MessageIds.Set((IMessage)target, id);
        }
    }
}
