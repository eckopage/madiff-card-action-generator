using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Text.Json;

namespace CardService.Api.Localization
{
    public class JsonStringLocalizer : IStringLocalizer
    {
        private readonly Dictionary<string, string> _localizationData;

        public JsonStringLocalizer()
        {
            var culture = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;
            var jsonFile = $"CardService.Api/Resources/localization.{culture}.json";
            
            if (File.Exists(jsonFile))
            {
                var jsonData = File.ReadAllText(jsonFile);
                _localizationData = JsonSerializer.Deserialize<Dictionary<string, string>>(jsonData);
            }
            else
            {
                _localizationData = new Dictionary<string, string>();
            }
        }

        public LocalizedString this[string name]
        {
            get
            {
                if (_localizationData.TryGetValue(name, out var value))
                {
                    return new LocalizedString(name, value);
                }
                return new LocalizedString(name, name, true);
            }
        }

        public LocalizedString this[string name, params object[] arguments] => this[name];

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            return new List<LocalizedString>();
        }
    }
}