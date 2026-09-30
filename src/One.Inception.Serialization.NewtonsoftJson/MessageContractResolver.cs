using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using One.Inception.Serialization.Newtonsofst.Jsson;
using System;
using System.Collections.Generic;

namespace One.Inception.Serialization.NewtonsoftJson
{
    public class MessageContractResolver : DataMemberContractResolver
    {
        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> properties = base.CreateProperties(type, memberSerialization);

            if (typeof(IMessage).IsAssignableFrom(type) && type.IsInterface == false)
                properties.Add(MessageIdValueProvider.CreateProperty(type));

            return properties;
        }
    }
}
