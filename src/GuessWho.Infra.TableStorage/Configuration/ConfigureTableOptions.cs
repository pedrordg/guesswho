using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace GuessWho.Infra.TableStorage.Configuration
{
    public class ConfigureTableOptions : IConfigureOptions<TableOptions>
    {
        /// <summary>
        /// The configuration
        /// </summary>
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigureBlobOptions"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public ConfigureTableOptions(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Invoked to configure a <typeparamref name="TOptions" /> instance.
        /// </summary>
        /// <param name="options">The options instance to configure.</param>
        public void Configure(TableOptions options)
        {
            _configuration.GetSection("AzureTable").Bind(options);
        }
    }
}
