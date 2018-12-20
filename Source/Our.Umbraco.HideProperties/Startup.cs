namespace Our.Umbraco.HideProperties
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Routing;

    using AutoMapper;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Logging;
    using global::Umbraco.Core.Persistence.Migrations;
    using global::Umbraco.Web;
    using global::Umbraco.Web.UI.JavaScript;

    using Our.Umbraco.HideProperties.Constants;
    using Our.Umbraco.HideProperties.Controllers.ApiControllers;
    using Our.Umbraco.HideProperties.Mapping.Profile;

    using Semver;

    public class Startup : ApplicationEventHandler
    {
        protected override void ApplicationStarted(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            using (ApplicationContext.Current.ProfilingLogger.TraceDuration<Startup>("Begin ApplicationStarted", "End ApplicationStarted"))
            {
                base.ApplicationStarted(umbracoApplication, applicationContext);

                this.SetupMigration();

                Mapper.AddProfile<RuleProfile>();

                ServerVariablesParser.Parsing += this.ServerVariablesParserParsing;
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

        private void ServerVariablesParserParsing(object sender, Dictionary<string, object> e)
        {
            if (HttpContext.Current == null)
            {
                throw new InvalidOperationException("HttpContext is null");
            }

            if (!e.Keys.Contains("hideProperties"))
            {
                var urlHelper = new UrlHelper(new RequestContext(new HttpContextWrapper(HttpContext.Current), new RouteData()));

                var urlDictionairy = new Dictionary<string, object>
                {
                    { "getRules", urlHelper.GetUmbracoApiService<RulesApiController>("Get") },
                    { "saveRule", urlHelper.GetUmbracoApiService<RulesApiController>("Post") }
                };

                e.Add("hideProperties", urlDictionairy);
            }
        }
    }
}
