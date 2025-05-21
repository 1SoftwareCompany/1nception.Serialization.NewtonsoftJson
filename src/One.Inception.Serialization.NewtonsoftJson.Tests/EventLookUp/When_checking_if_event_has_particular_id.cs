using System;
using System.Collections.Generic;
using System.Linq;
using Machine.Specifications;
using One.Inception.Serialization.NewtonsoftJson.Tests.EventLookUp.Models;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.custom_cases
{
    [Subject(typeof(EventLookupInByteArray))]
    public class When_checking_if_event_has_particular_id_and_it_is_successful
    {
        static JsonSerializer serializer;
        static EventLookupInByteArray eventLookUp;

        static byte[] firstEventData;
        static byte[] firstEventIdBytes;

        static bool found;

        Establish context = () =>
        {
            var contracts = new List<Type>();

            contracts.AddRange(typeof(FirstEvent).Assembly.GetExportedTypes());

            TypeContainer<IEvent> typeContainer = new TypeContainer<IEvent>(contracts);

            serializer = new JsonSerializer(contracts);
            eventLookUp = new EventLookupInByteArray(typeContainer, new TypeContainer<IPublicEvent>(Enumerable.Empty<Type>()));
            firstEventIdBytes = System.Text.Encoding.UTF8.GetBytes(FirstEvent.Contract);

            var fistEvent = new FirstEvent("firstId", DateTimeOffset.UtcNow);
            firstEventData = serializer.SerializeToBytes(fistEvent);
        };

        Because of = () => found = eventLookUp.HasEventId(firstEventData, firstEventIdBytes);

        It should_be_true = () => found.ShouldBeTrue();
    }


    [Subject(typeof(EventLookupInByteArray))]
    public class When_checking_if_event_has_particular_id_and_it_does_not_match
    {
        static JsonSerializer serializer;
        static EventLookupInByteArray eventLookUp;

        static byte[] firstEventData;
        static byte[] secondEventIdBytes;

        static bool found;

        Establish context = () =>
        {
            var contracts = new List<Type>();

            contracts.AddRange(typeof(FirstEvent).Assembly.GetExportedTypes());

            TypeContainer<IEvent> typeContainer = new TypeContainer<IEvent>(contracts);

            serializer = new JsonSerializer(contracts);
            eventLookUp = new EventLookupInByteArray(typeContainer, new TypeContainer<IPublicEvent>(Enumerable.Empty<Type>()));

            secondEventIdBytes = System.Text.Encoding.UTF8.GetBytes(SecondEvent.Contract);

            var fistEvent = new FirstEvent("firstId", DateTimeOffset.UtcNow);
            firstEventData = serializer.SerializeToBytes(fistEvent);
        };

        Because of = () => found = eventLookUp.HasEventId(firstEventData, secondEventIdBytes);

        It should_be_false = () => found.ShouldBeFalse();
    }

    [Subject(typeof(EventLookupInByteArray))]
    public class When_checking_if_event_has_particular_id_for_nested_event_and_is_successful
    {
        static JsonSerializer serializer;
        static EventLookupInByteArray eventLookUp;

        static byte[] nestedEventData;
        static byte[] nestedEventId;

        static bool found;

        Establish context = () =>
        {
            var contracts = new List<Type>();

            contracts.AddRange(typeof(FirstEvent).Assembly.GetExportedTypes());

            TypeContainer<IEvent> typeContainer = new TypeContainer<IEvent>(contracts);

            serializer = new JsonSerializer(contracts);
            eventLookUp = new EventLookupInByteArray(typeContainer, new TypeContainer<IPublicEvent>(Enumerable.Empty<Type>()));
            nestedEventId = System.Text.Encoding.UTF8.GetBytes(NestedEvent.Contract);

            var secondEvent = new SecondEvent("secondId", DateTimeOffset.UtcNow);
            var nestedEvent = new NestedEvent("nestedId", secondEvent, DateTimeOffset.UtcNow);

            nestedEventData = serializer.SerializeToBytes(nestedEvent);
        };

        Because of = () => found = eventLookUp.HasEventId(nestedEventData, nestedEventId);

        It should_be_true = () => found.ShouldBeTrue();
    }

    [Subject(typeof(EventLookupInByteArray))]
    public class When_checking_if_event_has_particular_id_for_event_that_has_invalid_contract_id
    {
        static JsonSerializer serializer;
        static EventLookupInByteArray eventLookUp;

        static byte[] invalidContractEventData;
        static byte[] invalidContractIdBytes;

        static bool found;

        Establish context = () =>
        {
            var contracts = new List<Type>();

            contracts.AddRange(typeof(InvalidContractEvent).Assembly.GetExportedTypes());

            TypeContainer<IEvent> typeContainer = new TypeContainer<IEvent>(contracts);

            serializer = new JsonSerializer(contracts);
            eventLookUp = new EventLookupInByteArray(typeContainer, new TypeContainer<IPublicEvent>(Enumerable.Empty<Type>()));
            var invalidContractEvent = new InvalidContractEvent("invalidContractId", DateTimeOffset.UtcNow);

            invalidContractIdBytes = System.Text.Encoding.UTF8.GetBytes(InvalidContractEvent.Contract);
            invalidContractEventData = serializer.SerializeToBytes(invalidContractEvent);
        };

        Because of = () => found = eventLookUp.HasEventId(invalidContractEventData, invalidContractIdBytes);

        It should_be_empty = () => found.ShouldBeFalse();
    }
}
