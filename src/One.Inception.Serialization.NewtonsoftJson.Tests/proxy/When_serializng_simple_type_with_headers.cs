﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Machine.Specifications;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.proxy
{
    [DataContract(Name = "cb14a430-4669-497e-a037-8e5be206679a")]
    public class BaseClass
    {
        public BaseClass()
        {

        }
        [DataMember(Order = 1)]
        private string BaseProperty { get; set; }
        public void SetStuff()
        {
            BaseProperty = "base value";
        }
    }

    [DataContract(Name = "a694dac8-0c1f-4cfe-a8a8-a5381c2ac44e")]
    public class ToProxy : BaseClass
    {
        [DataMember(Order = 2)]
        public string TestProperty { get; set; }
    }

    [Subject(typeof(JsonSerializer))]
    public class When_serializng_simple_type_with_headers
    {
        Establish context = () =>
        {
            ser = new ToProxy() { TestProperty = "aaaa" };
            ser.SetStuff();
            var contracts = new List<Type>();
            contracts.AddRange(typeof(NestedType).Assembly.GetExportedTypes());
            contracts.AddRange(typeof(When_serializng_simple_type_with_headers).Assembly.GetExportedTypes());
            serializer = new JsonSerializer(contracts);
            data = serializer.SerializeToBytes(ser);
        };

        Because of_deserialization = () => deser = serializer.DeserializeFromBytes<ToProxy>(data);

        It should_not_be_null = () => deser.ShouldNotBeNull();

        It should_be_of_exact_value = () => typeof(BaseClass).GetProperty("BaseProperty", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance).GetValue(deser).ShouldEqual("base value");


        static ToProxy ser;
        static ToProxy deser;
        static JsonSerializer serializer;
        static byte[] data;
    }
}
