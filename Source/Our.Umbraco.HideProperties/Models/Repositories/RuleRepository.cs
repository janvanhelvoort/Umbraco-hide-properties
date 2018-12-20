namespace Our.Umbraco.HideProperties.Models.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using global::Umbraco.Core;
    using global::Umbraco.Core.Persistence;
    using global::Umbraco.Core.Persistence.SqlSyntax;

    using Our.Umbraco.HideProperties.Constants;
    using Our.Umbraco.HideProperties.Models.Pocos;

    internal class RuleRepository
    {
        public static readonly RuleRepository Current = new RuleRepository();

        public Database Database => ApplicationContext.Current.DatabaseContext.Database;

        public ISqlSyntaxProvider SqlSyntax => ApplicationContext.Current.DatabaseContext.SqlSyntax;

        public IEnumerable<Rule> Get()
        {
            var query = new Sql().Select("*").From(TableConstants.Rules.TableName);

            return this.Database.Fetch<Rule>(query);
        }

        public Rule GetByKey(int key)
        {
            var query = new Sql().Select("*").From(TableConstants.Rules.TableName).Where<Rule>(x => x.Key.Equals(key), this.SqlSyntax);

            return this.Database.Fetch<Rule>(query).FirstOrDefault();
        }

        public Rule Save(Rule rule)
        {
            if (rule != null)
            {
                if (rule.Key != Guid.Empty)
                {
                    this.Database.Update(rule);
                }
                else
                {
                    rule.Key = Guid.NewGuid();
                    this.Database.Save(rule);
                }
            }

            return rule;
        }
    }
}
