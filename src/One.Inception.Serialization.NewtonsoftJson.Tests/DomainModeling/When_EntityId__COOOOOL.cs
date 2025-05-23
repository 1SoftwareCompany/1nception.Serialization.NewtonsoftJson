﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Machine.Specifications;

namespace One.Inception.Serialization.NewtonsoftJson.Tests.custom_cases
{
    [Subject(typeof(JsonSerializer))]
    public class When_EntityId__COOOOOL
    {
        static Guid WorkGuid = new Guid("eeceebfd-9f2f-4c0a-8242-6d7df1470d13");
        static Guid JobId = new Guid("debd3009-120b-45d9-84ba-e2216f597de0");
        Establish context = () =>
        {
            ser = new WorkId(WorkGuid, new JobId(JobId));
            var contracts = new List<Type>();
            contracts.AddRange(typeof(WorkId).Assembly.GetExportedTypes());
            serializer = new JsonSerializer(contracts);
            data = serializer.SerializeToBytes(ser);
        };
        Because of_deserialization = () => deser = serializer.DeserializeFromBytes<WorkId>(data);

        It should_not_be_null = () => deser.ShouldNotBeNull();

        static WorkId ser;
        static WorkId deser;
        static JsonSerializer serializer;
        static byte[] data;
    }

    [DataContract(Name = "33908fe3-89d8-458f-975f-4a1e273c2134")]
    public class WorkId : EntityId<JobId>
    {
        protected override ReadOnlySpan<char> EntityName => "Work";

        protected WorkId() { }
        public WorkId(Guid id, JobId jobId) : base(id.ToString(), jobId) { }
    }

    [DataContract(Name = "470532ba-fe38-4dd4-b825-26c81d75a64e")]
    public class JobId : AggregateRootId
    {
        protected JobId() { }
        public JobId(Guid id) : base("testnamespace", "Job", id.ToString()) { }
    }
}
