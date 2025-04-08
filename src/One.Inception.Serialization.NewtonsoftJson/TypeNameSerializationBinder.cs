﻿using System;
using System.Collections.Generic;
using System.Reflection;
using Newtonsoft.Json.Serialization;

namespace One.Inception.Serialization.NewtonsoftJson
{
    public sealed class TypeNameSerializationBinder : ISerializationBinder
    {
        static Assembly DotNetAssembly = typeof(object).Assembly;

        private readonly ContractsRepository contractRepository;

        public TypeNameSerializationBinder(IEnumerable<Type> contracts)
        {
            this.contractRepository = new ContractsRepository(contracts);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            string name;
            if (contractRepository.TryGet(serializedType, out name))
            {
                assemblyName = null;
                typeName = name;
            }
            else
            {
                if (serializedType.Assembly == DotNetAssembly)
                {
                    assemblyName = serializedType.Assembly.FullName;
                    typeName = serializedType.FullName;
                }
                else if (serializedType.IsGenericType)
                {
                    var contractId = contractRepository.GetGenericTypeContract(serializedType);
                    contractRepository.Map(serializedType, contractId);

                    assemblyName = null;
                    typeName = contractId;
                }
                else
                {
                    throw new InvalidOperationException(String.Format("Unkown, unregistered type {0}", serializedType));
                }
            }
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            try
            {
                if (assemblyName == null)
                {
                    Type type;
                    if (contractRepository.TryGet(typeName, out type))
                    {
                        return type;
                    }
                    else
                    {
                        var genericType = contractRepository.GetGenericType(typeName);
                        if (genericType is not null)
                        {
                            contractRepository.Map(genericType, typeName);

                            return genericType;
                        }
                    }
                }
                return Type.GetType(string.Format("{0}, {1}", typeName, assemblyName), true);
            }
            catch (TypeLoadException)
            {
                throw new InvalidOperationException(String.Format("Unkown, unregistered type {0}:'{1}'", assemblyName, typeName));
            }
        }
    }
}
