

using ARC.Infrastructure.Localization.Models;
using Microsoft.Extensions.Options;

namespace ARC.Infrastructure.Localization
{
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IDistributedCache _cache;
        private readonly IOptions<LocalizationSettings> _localizationSettings;

        public JsonStringLocalizerFactory(IDistributedCache cache, IOptions<LocalizationSettings> localizationSettings)
        {
            _cache = cache;
            _localizationSettings = localizationSettings;
        }

        public IStringLocalizer Create(Type resourceSource)
        {
            return new JsonStringLocalizer(_cache, _localizationSettings);
        }

        public IStringLocalizer Create(string baseName, string location)
        {
            return new JsonStringLocalizer(_cache, _localizationSettings);
        }
    }
}
