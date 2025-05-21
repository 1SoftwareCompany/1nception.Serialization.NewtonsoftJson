using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using One.Inception.Discoveries;
using One.Inception.Serialization.NewtonsoftJson;
using One.Inception.Serializer;

namespace One.Inception.Pipeline.Config
{
    public class JsonSerializerDiscovery : DiscoveryBase<ISerializer>
    {
        protected override DiscoveryResult<ISerializer> DiscoverFromAssemblies(DiscoveryContext context)
        {
            return new DiscoveryResult<ISerializer>(GetModels(context));
        }

        IEnumerable<DiscoveredModel> GetModels(DiscoveryContext context)
        {
            yield return new DiscoveredModel(typeof(ISerializer), GetSerializer(context)) { CanOverrideDefaults = true }; // Singleton
            yield return new DiscoveredModel(typeof(IEventLookUp), typeof(EventLookupInByteArray), ServiceLifetime.Singleton) { CanOverrideDefaults = true };
        }

        protected virtual ISerializer GetSerializer(DiscoveryContext context)
        {
            IEnumerable<Type> contracts = context.Assemblies
                .SelectMany(ass => ass.GetLoadableTypes());

            return new JsonSerializer(contracts);
        }
    }
}

