namespace Our.Umbraco.HideProperties.Controllers.ApiControllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using global::Umbraco.Web.Editors;

    using Our.Umbraco.HideProperties.Attributes;
    using Our.Umbraco.HideProperties.CacheRefresher;
    using Our.Umbraco.HideProperties.Models;
    using Our.Umbraco.HideProperties.Services;

    [CamelCase]
    public class RulesApiController : UmbracoAuthorizedJsonController
    {
        public object QuestionRepository { get; private set; }

        [HttpGet]
        public HttpResponseMessage Get()
        {
            return this.Request.CreateResponse(HttpStatusCode.OK, RuleService.Current.GetRules());
        }

        [HttpPost]
        public HttpResponseMessage Post(Rule rule)
        {
            return this.UpdateRule(rule) != null ?
                this.Request.CreateResponse(HttpStatusCode.OK, rule) :
                this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't save rule");
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var rule = RuleService.Current.GetById(id);

            if (rule != null)
            {
                rule.IsDeleted = true;
                return this.UpdateRule(rule) != null ?
                    this.Request.CreateResponse(HttpStatusCode.OK, rule) :
                    this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't delete rule");
            }

            return this.Request.CreateErrorResponse(HttpStatusCode.NotFound, "Can't find rule");
        }

        private Rule UpdateRule(Rule rule)
        {
            rule = RuleService.Current.Save(rule);

            if (rule != null)
            {
                RuleCacheRefresher.ClearCache();
            }

            return rule;
        }
    }
}
