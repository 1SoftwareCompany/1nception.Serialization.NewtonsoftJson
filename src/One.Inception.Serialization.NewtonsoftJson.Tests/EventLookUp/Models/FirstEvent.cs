using System;
using System.Runtime.Serialization;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.EventLookUp.Models
{
    [DataContract(Name = Contract)]
    public class FirstEvent : IEvent
    {
        public const string Contract = "3367f02b-1c0a-474e-b142-195ee28e1d61";

        public FirstEvent()
        {

        }

        public FirstEvent(string id, DateTimeOffset timestamp)
        {
            Id = id;
            Timestamp = timestamp;
        }

        [DataMember(Order = 1)]
        public string Id { get; set; }

        [DataMember(Order = 2)]
        public DateTimeOffset Timestamp { get; set; }
    }
}
