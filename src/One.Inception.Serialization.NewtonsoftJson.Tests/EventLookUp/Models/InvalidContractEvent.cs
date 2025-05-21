using System;
using System.Runtime.Serialization;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.EventLookUp.Models
{
    [DataContract(Name = Contract)]
    public class InvalidContractEvent : IEvent
    {
        public const string Contract = "1c1a7bba-0665-4fbf";

        public InvalidContractEvent()
        {

        }

        public InvalidContractEvent(string id, DateTimeOffset timestamp)
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
