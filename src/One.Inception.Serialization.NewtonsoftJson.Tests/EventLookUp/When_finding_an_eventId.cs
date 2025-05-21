using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using One.Inception.Serialization.NewtonsoftJson.Tests.EventLookUp.Models;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.custom_cases
{
    [Subject(typeof(EventLookupInByteArray))]
    public class When_finding_an_eventId_and_it_is_successful
    {
        static JsonSerializer serializer;
        static EventLookupInByteArray eventLookUp;

        static byte[] firstEventData;

        static string foundContract;

        Establish context = () =>
        {
            var contracts = new List<Type>();

            contracts.AddRange(typeof(FirstEvent).Assembly.GetExportedTypes());

            TypeContainer<IEvent> typeContainer = new TypeContainer<IEvent>(contracts);

            serializer = new JsonSerializer(contracts);
            eventLookUp = new EventLookupInByteArray(typeContainer, new TypeContainer<IPublicEvent>(Enumerable.Empty<Type>()));

            var fistEvent = new FirstEvent("firstId", DateTimeOffset.UtcNow);
            firstEventData = serializer.SerializeToBytes(fistEvent);
        };

        Because of = () => foundContract = eventLookUp.FindEventId(firstEventData);

        It should_equal = () => foundContract.ShouldEqual(FirstEvent.Contract);
    }

    [Subject(typeof(EventLookupInByteArray))]
    public class When_finding_an_eventId_for_nested_event
    {
        static JsonSerializer serializer;
        static EventLookupInByteArray eventLookUp;

        static byte[] nestedEventData;

        static string foundContract;

        Establish context = () =>
        {
            var contracts = new List<Type>();

            contracts.AddRange(typeof(FirstEvent).Assembly.GetExportedTypes());

            TypeContainer<IEvent> typeContainer = new TypeContainer<IEvent>(contracts);

            serializer = new JsonSerializer(contracts);
            eventLookUp = new EventLookupInByteArray(typeContainer, new TypeContainer<IPublicEvent>(Enumerable.Empty<Type>()));

            var secondEvent = new SecondEvent("secondId", DateTimeOffset.UtcNow);
            var nestedEvent = new NestedEvent("nestedId", secondEvent, DateTimeOffset.UtcNow);

            nestedEventData = serializer.SerializeToBytes(nestedEvent);
        };

        Because of = () => foundContract = eventLookUp.FindEventId(nestedEventData);

        It should_equal = () => foundContract.ShouldEqual(NestedEvent.Contract);
    }

    [Subject(typeof(EventLookupInByteArray))]
    public class When_finding_an_eventId_for_event_that_has_invalid_contract_id
    {
        static JsonSerializer serializer;
        static EventLookupInByteArray eventLookUp;

        static byte[] invalidContractEventData;

        static string foundContract;

        Establish context = () =>
        {
            var contracts = new List<Type>();

            contracts.AddRange(typeof(InvalidContractEvent).Assembly.GetExportedTypes());

            TypeContainer<IEvent> typeContainer = new TypeContainer<IEvent>(contracts);

            serializer = new JsonSerializer(contracts);
            eventLookUp = new EventLookupInByteArray(typeContainer, new TypeContainer<IPublicEvent>(Enumerable.Empty<Type>()));
            var invalidContractEvent = new InvalidContractEvent("invalidContractId", DateTimeOffset.UtcNow);

            invalidContractEventData = serializer.SerializeToBytes(invalidContractEvent);
        };

        Because of = () => foundContract = eventLookUp.FindEventId(invalidContractEventData);

        It should_be_empty = () => foundContract.ShouldEqual(string.Empty);
    }
}
