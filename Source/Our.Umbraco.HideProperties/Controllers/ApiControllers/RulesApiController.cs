namespace Our.Umbraco.HideProperties.Controllers.ApiControllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using global::Umbraco.Web.Editors;

    using Our.Umbraco.HideProperties.Attributes;
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
            using (var transaction = this.ApplicationContext.DatabaseContext.Database.GetTransaction())
            {
                rule = RuleService.Current.Save(rule);

                if (rule != null)
                {
                    transaction.Complete();

                    return this.Request.CreateResponse(HttpStatusCode.OK, rule);
                }

                return this.Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Can't save rule");
            }                
        }
    }
}
