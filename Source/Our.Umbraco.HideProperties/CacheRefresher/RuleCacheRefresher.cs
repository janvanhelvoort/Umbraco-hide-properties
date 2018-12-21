namespace Our.Umbraco.HideProperties.CacheRefresher
{
    using System;

    using global::Umbraco.Core.Cache;
    using global::Umbraco.Web.Cache;

    using Our.Umbraco.HideProperties.Constants;

    public class RuleCacheRefresher : JsonCacheRefresherBase<RuleCacheRefresher>
    {
        protected override RuleCacheRefresher Instance
        {
            get { return this; }
        }

        public override Guid UniqueIdentifier
        {
            get { return CacheRefresherConstants.RuleCacheRefreshGuid; }
        }

        public override string Name
        {
            get { return "Rule cache refresher"; }
        }

        public static void ClearCache()
        {
            DistributedCache.Instance.RefreshAll(CacheRefresherConstants.RuleCacheRefreshGuid);
        }
    }
}
