﻿using System;
using System.Runtime.Serialization;

namespace One.Inception.Serialization.NewtonsoftJson.Tests
{
    [DataContract(Name = "8ba2c7c8-9116-4969-9244-ffde4bbd39cd")]
    public class NestedTypeWithNonDataContractTypes
    {
        [DataMember(Order = 1)]
        public string String { get; set; }

        [DataMember(Order = 2)]
        public int Int { get; set; }

        [DataMember(Order = 3)]
        public DateTime Date { get; set; }

        [DataMember(Order = 4)]
        public object Nested { get; set; }
    }
}
