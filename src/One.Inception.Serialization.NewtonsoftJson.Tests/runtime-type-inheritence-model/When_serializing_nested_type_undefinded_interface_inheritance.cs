﻿using System;
using System.Collections.Generic;
using Machine.Specifications;

namespace One.Inception.Serialization.NewtonsoftJson.Tests
{
    [Subject(typeof(JsonSerializer))]
    public class When_serializing_nested_type_undefinded_interface_inheritance
    {
        Establish context = () =>
        {
            ser = new NestedTypeWithUndefinedInterfaceInheritance() { Int = 5, Date = DateTime.UtcNow.AddDays(1), String = "a", Nested = new UndefinedInterfaceInheritance() { Int = 4, Date = DateTime.UtcNow.AddDays(2), String = "b" } };
            var contracts = new List<Type>();
            contracts.AddRange(typeof(NestedType).Assembly.GetExportedTypes());
            serializer = new JsonSerializer(contracts);
            data = serializer.SerializeToBytes(ser);
        };

        Because of_deserialization = () => deser = serializer.DeserializeFromBytes<NestedTypeWithUndefinedInterfaceInheritance>(data);

        It should_not_be_null = () => deser.ShouldNotBeNull();
        It should_have_the_same_int = () => deser.Int.ShouldEqual(ser.Int);
        It should_have_the_same_string = () => deser.String.ShouldEqual(ser.String);
        It should_have_the_same_date = () => deser.Date.ShouldEqual(ser.Date);
        It should_have_the_same_date_as_utc = () => deser.Date.ToFileTimeUtc().ShouldEqual(ser.Date.ToFileTimeUtc());

        It nested_object_should_not_be_null = () => deser.Nested.ShouldNotBeNull();
        It nested_object_should_be_of_the_right_type = () => deser.Nested.ShouldBeOfExactType(typeof(UndefinedInterfaceInheritance));

        static NestedTypeWithUndefinedInterfaceInheritance ser;
        static NestedTypeWithUndefinedInterfaceInheritance deser;
        static JsonSerializer serializer;
        static byte[] data;
    }
}
