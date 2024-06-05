using FPLSP_Analyst.Application.Interfaces.Services;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;

namespace FPLSP_Analyst.Infrastructure.Implements.Services
{
    public class LocalizationService : ILocalizationService
    {
        private readonly IStringLocalizer _stringLocalizer;

        public LocalizedString this[string name] => _stringLocalizer[name];

        public LocalizedString this[string name, params object[] arguments] => _stringLocalizer[name, arguments];

        public CultureInfo CurrentCulture => CultureInfo.CurrentCulture;

        public LocalizationService(IStringLocalizerFactory factory)
        {
            _stringLocalizer = factory.Create("AppResource", Assembly.GetEntryAssembly()?.FullName ?? string.Empty);
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return _stringLocalizer.GetAllStrings(includeParentCultures);
        }
    }
}
