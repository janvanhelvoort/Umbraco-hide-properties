﻿namespace Our.Umbraco.HideProperties
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
            this.IsExportEnabled = this.GetAppSetting(Constants.AppSettings.EnableExport, true);
            this.ExportOnSave = this.GetAppSetting(Constants.AppSettings.ExportOnSave, false);

            this.IsImportEnabled = this.GetAppSetting(Constants.AppSettings.EnableImport, true);
            this.ImportAtStartup = this.GetAppSetting(Constants.AppSettings.ImportAtStartup, false);

            instance = this;
        }

        /// <summary>
        /// Gets the current context
        /// </summary>
        public static HidePropertiesConfig Current => instance ?? new HidePropertiesConfig();

        // Is export enabled
        public bool IsExportEnabled { get; set; }

        // Export on save
        public bool ExportOnSave { get; set; }

        // Is import enabled
        public bool IsImportEnabled { get; set; }

        // Import at startup
        public bool ImportAtStartup { get; set; }

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
