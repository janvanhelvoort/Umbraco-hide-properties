namespace Our.Umbraco.HideProperties.Controllers.ApiControllers
{
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    using global::Umbraco.Web.Editors;

    public class HidePropertiesApiController : UmbracoAuthorizedJsonController
    {
        [HttpGet]
        public HttpResponseMessage Export()
        {
            HidePropertiesContext.Current.ExportRules();

            return this.Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
