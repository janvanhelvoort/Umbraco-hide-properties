namespace Our.Umbraco.HideProperties.EventHandlers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http.Filters;

    using global::Umbraco.Core;
    using global::Umbraco.Web;
    using global::Umbraco.Web.Editors;
    using global::Umbraco.Web.Models.ContentEditing;

    using Our.Umbraco.HideProperties.Models;
    using Our.Umbraco.HideProperties.Services;

    internal static class EditorModelEventManagerEventHandler
    {
        public static void SendingContentModel(HttpActionExecutedContext sender, EditorModelEventArgs<ContentItemDisplay> e)
        {
            var contentItemDisplay = e.Model;
            var userGroupAliasses = e.UmbracoContext.Security.CurrentUser.Groups.Select(userGroup => userGroup.Alias).ToList();

            var contentType = ApplicationContext.Current.Services.ContentTypeService.GetContentType(contentItemDisplay.ContentTypeAlias);

            var rules = RuleService.Current.GetActiveRules(contentType.CompositionAliases().Concat(new List<string> { contentItemDisplay.ContentTypeAlias }));

            foreach (var rule in rules)
            {
                if (rule.UserGroups.Any(userGroup => userGroupAliasses.Any(alias => alias.Equals(userGroup))))
                {
                    HideTabs(contentItemDisplay, userGroupAliasses, rule);
                    HideProperties(contentItemDisplay, userGroupAliasses, rule);
                }
            }

            HideEmptyTabs(contentItemDisplay);
        }

        private static void HideTabs(ContentItemDisplay contentItemDisplay, List<string> userGroupAliasses, Rule rule)
        {
            if (rule.Tabs.Any())
            {
                contentItemDisplay.Tabs = contentItemDisplay.Tabs.Where(tab => !rule.Tabs.Contains(tab.Alias) && tab.Properties.Any());
            }
        }

        private static void HideProperties(ContentItemDisplay contentItemDisplay, List<string> userGroupAliasses, Rule rule)
        {
            if (rule.Properties.Any())
            {
                foreach (var tab in contentItemDisplay.Tabs)
                {
                    tab.Properties = tab.Properties.Where(property => !rule.Properties.Contains(property.Alias));
                }
            }
        }

        private static void HideEmptyTabs(ContentItemDisplay contentItemDisplay)
        {
            contentItemDisplay.Tabs = contentItemDisplay.Tabs.Where(tab => tab.Properties.Any());
        }
    }
}
