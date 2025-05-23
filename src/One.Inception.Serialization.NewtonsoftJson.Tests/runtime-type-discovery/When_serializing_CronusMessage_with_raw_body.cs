﻿using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace One.Inception.Serialization.NewtonsoftJson.Tests
{
    [Subject(typeof(JsonSerializer))]
    public class When_serializing_InceptionMessage_with_raw_body
    {
        Establish context = () =>
        {
            var contracts = new List<Type>();
            contracts.AddRange(typeof(NestedType).Assembly.GetExportedTypes());
            contracts.AddRange(typeof(InceptionMessage).Assembly.GetExportedTypes());
            serializer = new JsonSerializer(contracts);

            var body = new NestedTypeWithHeaders()
            {
                Int = 5,
                Date = DateTime.UtcNow.AddDays(1),
                String = "a",
                Nested = new SimpleNestedTypeWithHeaders()
                {
                    Int = 4,
                    Date = DateTime.UtcNow.AddDays(2),
                    String = "b"
                }
            };

            data = serializer.SerializeToBytes(body);

            ser = new InceptionMessage(data, typeof(NestedTypeWithHeaders), new Dictionary<string, string>());
            cmBytes = serializer.SerializeToBytes(ser);
        };

        Because of_deserialization = () => deser = serializer.DeserializeFromBytes<InceptionMessage>(cmBytes);

        It should_not_be_null = () => deser.ShouldNotBeNull();
        It should_have_the_same_byte_array = () => deser.PayloadRaw.AsSpan().SequenceEqual(ser.PayloadRaw.AsSpan()).ShouldBeTrue();
        It should_have_the_same_byte_array_length = () => deser.PayloadRaw.Length.ShouldEqual(ser.PayloadRaw.Length);

        static InceptionMessage ser;
        static InceptionMessage deser;
        static JsonSerializer serializer;
        static byte[] cmBytes;
        static byte[] data;
    }
}
