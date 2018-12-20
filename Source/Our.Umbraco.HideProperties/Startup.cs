namespace Our.Umbraco.HideProperties
{
    using global::Umbraco.Core;
    public class Startup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            using (ApplicationContext.Current.ProfilingLogger.TraceDuration<Startup>("Begin ApplicationStarted", "End ApplicationStarted"))
            {
                base.ApplicationStarted(umbracoApplication, applicationContext);
            }
        }
    }
}
