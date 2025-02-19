using Microsoft.Extensions.Localization;

namespace CardService.Api.Localization
{
    /// <summary>
    /// Factory for creating instances of <see cref="JsonStringLocalizer"/>.
    /// This class implements <see cref="IStringLocalizerFactory"/> to provide localized strings
    /// from JSON files based on the current culture.
    /// </summary>
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Initializes a new instance of <see cref="JsonStringLocalizerFactory"/>.
        /// </summary>
        /// <param name="serviceProvider">The service provider for dependency injection.</param>
        public JsonStringLocalizerFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Creates an instance of <see cref="JsonStringLocalizer"/> based on the given resource type.
        /// </summary>
        /// <param name="resourceSource">The type of the resource requesting localization.</param>
        /// <returns>An instance of <see cref="JsonStringLocalizer"/>.</returns>
        public IStringLocalizer Create(Type resourceSource)
        {
            return _serviceProvider.GetRequiredService<JsonStringLocalizer>();
        }

        /// <summary>
        /// Creates an instance of <see cref="JsonStringLocalizer"/> based on the given base name and location.
        /// </summary>
        /// <param name="baseName">The base name of the resource.</param>
        /// <param name="location">The location of the resource.</param>
        /// <returns>An instance of <see cref="JsonStringLocalizer"/>.</returns>
        public IStringLocalizer Create(string baseName, string location)
        {
            return _serviceProvider.GetRequiredService<JsonStringLocalizer>();
        }
    }
}