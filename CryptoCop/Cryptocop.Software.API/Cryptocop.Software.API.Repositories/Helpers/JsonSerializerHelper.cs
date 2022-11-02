﻿using System.Text.Json;

namespace Cryptocop.Software.API.Repositories.Helpers
{
    public static class JsonSerializerHelper
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        public static T? DeserializeWithCamelCasing<T>(string message) =>
            JsonSerializer.Deserialize<T>(message, _options);

        public static string SerializeWithCamelCasing<T>(T obj) =>
            JsonSerializer.Serialize(obj, _options);
    }
}
