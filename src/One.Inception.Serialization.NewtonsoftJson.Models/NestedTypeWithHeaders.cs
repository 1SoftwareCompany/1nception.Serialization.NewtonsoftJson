﻿using System;
using System.Runtime.Serialization;

namespace One.Inception.Serialization.NewtonsoftJson.Tests
{
    [DataContract(Name = "667f821e-4975-4295-a309-b399ae3f4c17")]
    public class NestedTypeWithHeaders : IMessage
    {
        [DataMember(Order = 1)]
        public string String { get; set; }

        [DataMember(Order = 2)]
        public int Int { get; set; }

        [DataMember(Order = 3)]
        public DateTime Date { get; set; }

        [DataMember(Order = 4)]
        public SimpleNestedTypeWithHeaders Nested { get; set; }

        [DataMember(Order = 5)]
        public DateTimeOffset Timestamp => DateTimeOffset.Now;
    }

    [DataContract(Name = "0d44bc1f-83c3-4747-a71e-fd38185b4d30")]
    public class SimpleNestedTypeWithHeaders
    {
        [DataMember(Order = 1)]
        public string String { get; set; }

        [DataMember(Order = 2)]
        public int Int { get; set; }

        [DataMember(Order = 3)]
        public DateTime Date { get; set; }

        [DataMember(Order = 4)]
        public SimpleType Nested { get; set; }
    }
}
