using System;
using System.Runtime.Serialization;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.EventLookUp.Models
{
    [DataContract(Name = Contract)]
    public class NestedEvent : IEvent
    {
        public const string Contract = "9e256ee4-2fa7-4a6c-8b03-d2758564ea69";

        public NestedEvent()
        {

        }

        public NestedEvent(string id, SecondEvent @event, DateTimeOffset timestamp)
        {
            Id = id;
            Event = @event;
            Timestamp = timestamp;
        }

        [DataMember(Order = 1)]
        public string Id { get; set; }

        [DataMember(Order = 2)]
        public SecondEvent Event { get; set; }

        [DataMember(Order = 3)]
        public DateTimeOffset Timestamp { get; set; }
    }
}
