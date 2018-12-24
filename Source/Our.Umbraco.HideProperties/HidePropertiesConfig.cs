namespace Our.Umbraco.HideProperties
{
    using System.Configuration;

    using global::Umbraco.Core;

    /// <summary>
    /// The hide-properties config
    /// </summary>
    public class HidePropertiesConfig
    {
        /// <summary>
        /// The current instance.
        /// </summary>
        private static HidePropertiesConfig instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="HidePropertiesContext"/> class from being created.
        /// </summary>
        private HidePropertiesConfig()
        {
            this.ExportOnSave = this.GetAppSetting(Constants.AppSettings.ExportOnSave, false);
            instance = this;
        }

        /// <summary>
        /// Gets the current context
        /// </summary>
        public static HidePropertiesConfig Current => instance ?? new HidePropertiesConfig();

        // Export on save
        public bool ExportOnSave { get; set; }

        /// <summary>
        /// Gets the value of app setting 
        /// </summary>
        /// <param name="key">The key.</param>
        /// <typeparam name="T">The return type</typeparam>
        /// <returns>The <see cref="T"/>.</returns>
        private T GetAppSetting<T>(string key, T defaultValue)
        {
            var setting = ConfigurationManager.AppSettings[key];

            if (setting != null)
            {
                var attempConvert = setting.TryConvertTo<T>();

                if (attempConvert.Success)
                {
                    return attempConvert.Result;
                }
            }

            return defaultValue;
        }
    }
}
