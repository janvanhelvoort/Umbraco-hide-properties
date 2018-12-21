namespace Our.Umbraco.HideProperties.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;

    using global::Umbraco.Core;

    using Our.Umbraco.HideProperties.Constants;
    using Our.Umbraco.HideProperties.Models;
    using Our.Umbraco.HideProperties.Models.Repositories;

    public class RuleService
    {
        public static readonly RuleService Current = new RuleService();

        public IEnumerable<Rule> Rules { get
            {
                return (IEnumerable<Rule>)ApplicationContext.Current.ApplicationCache.RuntimeCache.GetCacheItem(RuntimeCacheConstants.RuntimeCacheKeyPrefix, () =>
                {
                    return Mapper.Map<IEnumerable<Rule>>(RuleRepository.Current.Get());
                }, TimeSpan.FromMinutes(RuntimeCacheConstants.DefaultExpiration), true);                
            }
        }

        public IEnumerable<Rule> GetRules()
        {
            return this.Rules;
        }

        public IEnumerable<Rule> GetRules(string alias)
        {
            return this.Rules.Where(rule => rule.ContentTypeAlias.Equals(alias));
        }

        public IEnumerable<Rule> GetActiveRules(string alias)
        {
            return this.Rules.Where(rule => rule.IsActive && rule.ContentTypeAlias.Equals(alias));
        }

        public IEnumerable<Rule> GetActiveRules(IEnumerable<string> aliases)
        {
            return this.Rules.Where(rule => rule.IsActive && aliases.Contains(rule.ContentTypeAlias));
        }

        public Rule Save(Rule rule)
        {
            var poco = Mapper.Map<Models.Pocos.Rule>(rule);

            poco = RuleRepository.Current.Save(poco);

            return Mapper.Map<Rule>(poco);
        }
    }
}
