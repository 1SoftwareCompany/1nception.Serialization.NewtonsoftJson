﻿using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace One.Inception.Serialization.NewtonsoftJson.Tests
{
    [Subject(typeof(JsonSerializer))]
    public class When_serializing_nested_type_with_headers
    {
        Establish context = () =>
        {
            ser = new NestedTypeWithHeaders()
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
            var contracts = new List<Type>();
            contracts.AddRange(typeof(NestedType).Assembly.GetExportedTypes());
            serializer = new JsonSerializer(contracts);
            data = serializer.SerializeToBytes(ser);
        };

        Because of_deserialization = () => deser = serializer.DeserializeFromBytes<NestedTypeWithHeaders>(data);


        It should_not_be_null = () => deser.ShouldNotBeNull();
        It should_have_the_same_int = () => deser.Int.ShouldEqual(ser.Int);
        It should_have_the_same_string = () => deser.String.ShouldEqual(ser.String);
        It should_have_the_same_date = () => deser.Date.ShouldEqual(ser.Date);
        It should_have_the_same_date_as_utc = () => deser.Date.ToFileTimeUtc().ShouldEqual(ser.Date.ToFileTimeUtc());

        It nested_object_should_not_be_null = () => deser.Nested.ShouldNotBeNull();
        It nested_object_should_have_the_same_int = () => deser.Nested.Int.ShouldEqual(ser.Nested.Int);
        It nested_object_should_have_the_same_string = () => deser.Nested.String.ShouldEqual(ser.Nested.String);
        It nested_object_should_have_the_same_date = () => deser.Nested.Date.ShouldEqual(ser.Nested.Date);
        It nested_object_should_have_the_same_date_as_utc = () => deser.Nested.Date.ToFileTimeUtc().ShouldEqual(ser.Nested.Date.ToFileTimeUtc());

        static NestedTypeWithHeaders ser;
        static NestedTypeWithHeaders deser;
        static JsonSerializer serializer;
        static byte[] data;
    }
}
