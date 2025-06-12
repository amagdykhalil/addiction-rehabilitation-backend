using ARC.Shared.Keys;
using Newtonsoft.Json;
using System.Reflection;

namespace ARC.Infrastructure.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private const string RESOURCE_BASE_PATH = "ARC.Shared.Resources";
        private readonly IDistributedCache _cache;
        private readonly JsonSerializer _serializer = new();
        private readonly Assembly _resourcesAssembly;

        public JsonStringLocalizer(IDistributedCache cache)
        {
            _cache = cache;
            _resourcesAssembly = typeof(LocalizationKeys).Assembly;
        }

        public LocalizedString this[string name]
        {
            get
            {
                var value = GetString(name);
                return new LocalizedString(name, value);
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                var actualValue = this[name];
                return !actualValue.ResourceNotFound
                    ? new LocalizedString(name, string.Format(actualValue.Value, arguments))
                    : actualValue;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var resourceName = GetResourceName(Thread.CurrentThread.CurrentCulture.Name);
            using var stream = _resourcesAssembly.GetManifestResourceStream(resourceName);
            if (stream == null) yield break;

            using var streamReader = new StreamReader(stream);
            using var reader = new JsonTextReader(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType != JsonToken.PropertyName)
                    continue;

                var key = reader.Value as string;
                reader.Read();
                var value = _serializer.Deserialize<string>(reader);
                yield return new LocalizedString(key, value);
            }
        }

        private string GetString(string key)
        {
            var resourceName = GetResourceName(Thread.CurrentThread.CurrentCulture.Name);
            var cacheKey = $"locale_{Thread.CurrentThread.CurrentCulture.Name}_{key}";
            var cacheValue = _cache.GetString(cacheKey);

            if (!string.IsNullOrEmpty(cacheValue))
                return cacheValue;

            var result = GetValueFromJSON(key, resourceName);

            if (!string.IsNullOrEmpty(result))
                _cache.SetString(cacheKey, result);

            return result;
        }

        private string GetValueFromJSON(string propertyName, string resourceName)
        {
            if (string.IsNullOrEmpty(propertyName) || string.IsNullOrEmpty(resourceName))
                return string.Empty;

            using var stream = _resourcesAssembly.GetManifestResourceStream(resourceName);
            if (stream == null) return string.Empty;

            using var streamReader = new StreamReader(stream);
            using var reader = new JsonTextReader(streamReader);

            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.PropertyName && reader.Value as string == propertyName)
                {
                    reader.Read();
                    return _serializer.Deserialize<string>(reader);
                }
            }

            return string.Empty;
        }

        private static string GetResourceName(string culture) => $"{RESOURCE_BASE_PATH}.{culture}.json";
    }
}
