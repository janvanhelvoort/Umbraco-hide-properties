﻿namespace Our.Umbraco.HideProperties.Models
{
    using System;
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public class Rule
    {
        public int Id { get; set; }

        public Guid Key { get; set; }

        public bool IsActive { get; set; }

        public string ContentTypeAlias { get; set; }

        public IEnumerable<string> Tabs { get; set; }

        public IEnumerable<string> Properties { get; set; }

        public IEnumerable<string> UserGroups { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}
