﻿using System;
using System.Collections.Generic;
using System.Linq;
using Elders.Cronus.Discoveries;
using Elders.Cronus.Serialization.NewtonsoftJson;

namespace Elders.Cronus.Pipeline.Config
{
    public class JsonSerializerDiscovery : DiscoveryBasedOnExecutingDirAssemblies<ISerializer>
    {
        protected ISerializer GetSerializer(DiscoveryContext context)
        {
            List<Type> contracts = context.Assemblies
                .Where(asm => ReferenceEquals(default(BoundedContextAttribute), asm.GetAssemblyAttribute<BoundedContextAttribute>()) == false)
                .SelectMany(ass => ass.GetExportedTypes())
                .ToList();

            return new JsonSerializer(contracts);
        }

        protected override DiscoveryResult<ISerializer> DiscoverFromAssemblies(DiscoveryContext context)
        {
            var result = new DiscoveryResult<ISerializer>();
            result.Models.Add(new DiscoveredModel(typeof(ISerializer), typeof(JsonSerializer), GetSerializer(context)));

            return result;
        }
    }
}

