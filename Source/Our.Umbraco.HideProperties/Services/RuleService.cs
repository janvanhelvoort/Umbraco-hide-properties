namespace Our.Umbraco.HideProperties.Services
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;

    using AutoMapper;

    using global::Umbraco.Core;

    using Our.Umbraco.HideProperties.Constants;
    using Our.Umbraco.HideProperties.Models;
    using Our.Umbraco.HideProperties.Models.Repositories;

    /// <summary>
    /// The rule server
    /// </summary>
    public class RuleService
    {
        /// <summary>
        /// The current instance.
        /// </summary>
        private static RuleService instance;

        /// <summary>
        /// Prevents a default instance of the <see cref="RuleService"/> class from being created.
        /// </summary>
        private RuleService()
        {
            instance = this;
        }

        /// <summary>
        /// Gets the current context
        /// </summary>
        public static RuleService Current => instance ?? new RuleService();

        // Rules
        private IEnumerable<Rule> Rules
        {
            get
            {
                return (IEnumerable<Rule>)ApplicationContext.Current.ApplicationCache.RuntimeCache.GetCacheItem(RuntimeCacheConstants.RuntimeCacheKeyPrefix, () =>
                {
                    return Mapper.Map<IEnumerable<Rule>>(RuleRepository.Current.GetBy(rule => !rule.IsDeleted));
                }, TimeSpan.FromMinutes(RuntimeCacheConstants.DefaultExpiration), true);
            }
        }

        // Get all rules
        public IEnumerable<Rule> GetRules()
        {
            return this.Rules;
        }

        // Get rules by alias
        public IEnumerable<Rule> GetRules(string alias)
        {
            return this.Rules.Where(rule => rule.ContentTypeAlias.Equals(alias));
        }

        // Get active rules by alias
        public IEnumerable<Rule> GetActiveRules(string alias)
        {
            return this.Rules.Where(rule => rule.IsActive && rule.ContentTypeAlias.Equals(alias));
        }

        // Get rules by multiple aliases
        public IEnumerable<Rule> GetActiveRules(IEnumerable<string> aliases)
        {
            return this.Rules.Where(rule => rule.IsActive && aliases.Contains(rule.ContentTypeAlias));
        }

        // Get by id
        public Rule GetById(int id)
        {
            return this.Rules.SingleOrDefault(rule => rule.Id.Equals(id));
        }

        // Add or update rule
        public Rule Save(Rule rule)
        {
            try
            {
                return Mapper.Map<Rule>(
                    RuleRepository.Current.Save(
                        Mapper.Map<Models.Pocos.Rule>(rule)));
            }
            catch (SqlException)
            {
                return null;
            }
        }
    }
}
