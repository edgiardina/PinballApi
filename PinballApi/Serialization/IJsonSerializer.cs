// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace PinballApi.Serialization
{
    public interface IJsonSerializer : ISerializer, IDeserializer
    {

    }
}