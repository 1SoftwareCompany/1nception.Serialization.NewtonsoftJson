﻿using System;
using System.Runtime.Serialization;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.Models
{
    [DataContract(Name = "f9e79cd5-3fd4-47e8-9b5e-b416ea222719")]
    public class ExtenralReferencedType
    {
        [DataMember(Order = 1)]
        public string String { get; set; }

        [DataMember(Order = 2)]
        public int Int { get; set; }

        [DataMember(Order = 3)]
        public DateTime Date { get; set; }
    }
}
