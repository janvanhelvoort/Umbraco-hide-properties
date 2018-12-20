namespace Our.Umbraco.HideProperties
{
    using System;
    using System.Linq;
    using global::Umbraco.Core;
    using global::Umbraco.Core.Logging;
    using global::Umbraco.Core.Persistence.Migrations;
    using global::Umbraco.Web;
    using Our.Umbraco.HideProperties.Constants;
    using Semver;
    public class Startup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            using (ApplicationContext.Current.ProfilingLogger.TraceDuration<Startup>("Begin ApplicationStarted", "End ApplicationStarted"))
            {
                base.ApplicationStarted(umbracoApplication, applicationContext);

                this.SetupMigration();
            }
        }

        private void SetupMigration()
        {
            var migrations = ApplicationContext.Current.Services.MigrationEntryService.GetAll(ApplicationConstants.ProductName);
            var latestMigration = migrations.OrderByDescending(x => x.Version).FirstOrDefault();

            var currentVersion = latestMigration != null ? latestMigration.Version : new SemVersion(0, 0, 0);

            var targetVersion = new SemVersion(0, 1, 0);
            if (targetVersion != currentVersion)
            {
                var migrationsRunner = new MigrationRunner(
                    ApplicationContext.Current.Services.MigrationEntryService,
                    ApplicationContext.Current.ProfilingLogger.Logger,
                    currentVersion,
                    targetVersion,
                    ApplicationConstants.ProductName);

                try
                {
                    migrationsRunner.Execute(UmbracoContext.Current.Application.DatabaseContext.Database);
                }
                catch (Exception e)
                {
                    LogHelper.Error<Startup>("Error running Statistics migration", e);
                }
            }
        }
    }
}
