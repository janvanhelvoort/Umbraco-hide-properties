namespace Our.Umbraco.HideProperties
{
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
    }
}
