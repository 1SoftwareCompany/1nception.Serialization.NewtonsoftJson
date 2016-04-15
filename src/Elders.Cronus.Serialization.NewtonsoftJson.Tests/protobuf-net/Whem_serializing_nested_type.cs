﻿using System;
using System.IO;
using Machine.Specifications;

namespace Elders.Cronus.Serialization.NewtonsoftJson.Tests
{
    [Subject("protobuff-net")]
    public class Whem_serializing_nested_type
    {
        Establish context = () =>
        {
            ser = new NestedType() { Int = 5, Date = DateTime.UtcNow.AddDays(1), String = "a", Nested = new SimpleNestedType() { Int = 4, Date = DateTime.UtcNow.AddDays(2), String = "b" } };
            serializer = new JsonSerializer((typeof(NestedType).Assembly));
            serStream = new MemoryStream();
            serializer.Serialize(serStream, ser);
            serStream.Position = 0;
        };
        Because of_deserialization = () => { deser = (NestedType)serializer.Deserialize(serStream); };


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

        static NestedType ser;
        static NestedType deser;
        static Stream serStream;
        static JsonSerializer serializer;
    }
}