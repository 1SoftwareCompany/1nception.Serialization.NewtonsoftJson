using System;
using System.Runtime.Serialization;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.EventLookUp.Models
{
    [DataContract(Name = Contract)]
    public class SecondEvent : IEvent
    {
        public const string Contract = "c1e82ca5-bbdf-43a4-9836-f9104c378dd0";

        public SecondEvent()
        {

        }

        public SecondEvent(string id, DateTimeOffset timestamp)
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
