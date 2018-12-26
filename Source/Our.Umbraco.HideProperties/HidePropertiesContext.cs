namespace Our.Umbraco.HideProperties
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    using global::Umbraco.Core.IO;
    using global::Umbraco.Core.Logging;

    using Newtonsoft.Json;

    using Our.Umbraco.HideProperties.Models.Pocos;
    using Our.Umbraco.HideProperties.Models.Repositories;

    /// <summary>
    /// The Hide-properties Context
    /// </summary>
    public class HidePropertiesContext
    {
        /// <summary>
        /// The current instance.
        /// </summary>
        private static HidePropertiesContext instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="HidePropertiesContext"/> class from being created.
        /// </summary>
        private HidePropertiesContext()
        {
            this.Configuration = HidePropertiesConfig.Current;
            instance = this;
        }

        /// <summary>
        /// Gets the current context
        /// </summary>
        public static HidePropertiesContext Current => instance ?? new HidePropertiesContext();

        // Configuration
        public HidePropertiesConfig Configuration { get; set; }

        /// <summary>
        /// Export Rules
        /// </summary>
        public void ExportRules()
        {
            try
            {
                if (this.Configuration.IsExportEnabled)
                {
                    var rulesFile = IOHelper.MapPath(Path.Combine(SystemDirectories.Config, "hideProperties.rules.js"));

                    if (File.Exists(rulesFile))
                    {
                        File.Delete(rulesFile);
                    }

                    using (var file = File.CreateText(rulesFile))
                    {
                        var serializer = new JsonSerializer { Formatting = Formatting.Indented };

                        serializer.Serialize(file, RuleRepository.Current.Get());
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.Warn<Startup>("Unable to save rules to disk: {0}", () => ex.ToString());
            }
        }

        public void ImportRules()
        {
            try
            {
                if (this.Configuration.IsImportEnabled)
                {
                    var rulesFile = IOHelper.MapPath(Path.Combine(SystemDirectories.Config, "hideProperties.rules.js"));

                    if (File.Exists(rulesFile))
                    {
                        var serializer = new JsonSerializer { Formatting = Formatting.Indented };

                        using (var file = new StreamReader(rulesFile))
                        {
                            using (var reader = new JsonTextReader(file))
                            {
                                var rules = serializer.Deserialize<IEnumerable<Rule>>(reader);
                            }
                        }
                    }
                    else
                    {
                        LogHelper.Warn<Startup>("Unable to import rules from disk: Missing hideProperties.rules.js file");
                    }                    
                }
            }
            catch (Exception ex)
            {
                LogHelper.Warn<Startup>("Unable to import rules from disk: {0}", () => ex.ToString());
            }
        }
    }
}
