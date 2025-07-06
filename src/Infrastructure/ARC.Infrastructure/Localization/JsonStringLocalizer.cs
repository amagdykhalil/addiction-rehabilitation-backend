using ARC.Infrastructure.Localization.Models;
using ARC.Shared.Keys;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace ARC.Infrastructure.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private const string RESOURCE_BASE_PATH = "ARC.Shared.Resources";
        private readonly IDistributedCache _cache;
        private readonly Assembly _resourcesAssembly;
        private readonly int _cacheExpirationDays;

        public JsonStringLocalizer(IDistributedCache cache, IOptions<LocalizationSettings> localizationSettings)
        {
            _cache = cache;
            _resourcesAssembly = typeof(LocalizationKeys).Assembly;
            _cacheExpirationDays = localizationSettings.Value.CacheExpirationDays;
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
            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            foreach (var resourceName in GetResourceNamesForCulture(culture, includeParentCultures))
            {
                var fileName = ExtractFileName(resourceName);
                using var stream = _resourcesAssembly.GetManifestResourceStream(resourceName);
                if (stream == null)
                    continue;

                using var reader = new StreamReader(stream);
                using var jsonReader = new JsonTextReader(reader);
                var jObject = JToken.ReadFrom(jsonReader);

                foreach (var kvp in FlattenJToken(jObject))
                {
                    yield return new LocalizedString($"{fileName}:{kvp.Key}", kvp.Value);
                }
            }
        }

        private string GetString(string key)
        {
            // Expecting key in format "fileName:nested.key.path"
            var split = key.Split(':', 2);
            if (split.Length != 2)
                return string.Empty;

            var fileName = split[0];
            var propertyPath = split[1];

            var culture = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            var cacheKey = $"locale_{culture}_{fileName}_{propertyPath}";
            var cacheValue = _cache.GetString(cacheKey);

            if (!string.IsNullOrEmpty(cacheValue))
                return cacheValue;

            var result = GetValueFromJSON(propertyPath, fileName, culture);

            if (!string.IsNullOrEmpty(result))
                _cache.SetString(cacheKey, result, new DistributedCacheEntryOptions
                { AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(_cacheExpirationDays) });

            return result ?? key;
        }

        private string GetValueFromJSON(string propertyPath, string fileName, string culture)
        {
            var resourceName = GetResourceName(culture, fileName);
            using var stream = _resourcesAssembly.GetManifestResourceStream(resourceName);
            if (stream == null)
                return string.Empty;

            using var reader = new StreamReader(stream);
            using var jsonReader = new JsonTextReader(reader);
            var jObject = JToken.ReadFrom(jsonReader);

            // Use JSONPath to directly select nested tokens
            var token = jObject.SelectToken(propertyPath);
            if (token == null)
                return string.Empty;

            // Return the raw string or JSON as needed
            if (token.Type == JTokenType.String || token.Type == JTokenType.Null)
                return token.ToString();

            return token.ToString(Formatting.None);
        }

        private static string GetResourceName(string culture, string fileName)
            => $"{RESOURCE_BASE_PATH}.{culture}.{fileName}.json";

        // Helper to get resource names for culture and parent cultures
        private IEnumerable<string> GetResourceNamesForCulture(string culture, bool includeParentCultures)
        {
            var cultures = new List<string> { culture };
            if (includeParentCultures && culture.Length > 2)
                cultures.Add(culture.Substring(0, 2)); // e.g., "ar-EG" -> "ar"

            var fileNames = new[] { "auth", "common", "globalException" }; // Add all your resource file names here
            foreach (var c in cultures)
            {
                foreach (var file in fileNames)
                    yield return GetResourceName(c, file);
            }
        }

        // Helper to flatten a JToken into key-value pairs with dot notation
        private IEnumerable<KeyValuePair<string, string>> FlattenJToken(JToken token, string prefix = "")
        {
            if (token.Type == JTokenType.Object)
            {
                foreach (var prop in token.Children<JProperty>())
                {
                    var childPrefix = string.IsNullOrEmpty(prefix) ? prop.Name : $"{prefix}.{prop.Name}";
                    foreach (var kvp in FlattenJToken(prop.Value, childPrefix))
                        yield return kvp;
                }
            }
            else if (token.Type == JTokenType.Array)
            {
                int i = 0;
                foreach (var item in token.Children())
                {
                    var childPrefix = $"{prefix}[{i}]";
                    foreach (var kvp in FlattenJToken(item, childPrefix))
                        yield return kvp;
                    i++;
                }
            }
            else
            {
                yield return new KeyValuePair<string, string>(prefix, token.ToString());
            }
        }

        private string ExtractFileName(string resourceName)
        {
            // Assumes resourceName ends with ".{fileName}.json"
            var parts = resourceName.Split('.');
            return parts.Length >= 2 ? parts[^2] : resourceName;
        }
    }
}
