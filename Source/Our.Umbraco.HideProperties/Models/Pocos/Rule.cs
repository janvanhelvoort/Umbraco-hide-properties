namespace Our.Umbraco.HideProperties.Models.Pocos
{
    using System;

    using global::Umbraco.Core.Persistence;

    using Our.Umbraco.HideProperties.Constants;

    [TableName(TableConstants.Rules.TableName)]

    internal class Rule
    {
        public int Id { get; set; }

        public Guid Key { get; set; }

        public bool IsActive { get; set; }

        public string ContentTypeAlias { get; set; }

        public string Tabs { get; set; }

        public string Properties { get; set; }

        public string UserGroups { get; set; }

        public bool IsDeleted { get; set; }
    }
}
