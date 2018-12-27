namespace Our.Umbraco.HideProperties.ContractResolver
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class IgnorableSerializerContractResolver : DefaultContractResolver
    {
        protected readonly Dictionary<Type, HashSet<string>> Ignores;

        public IgnorableSerializerContractResolver()
        {
            this.Ignores = new Dictionary<Type, HashSet<string>>();
        }

        public void Ignore(Type type, params string[] propertyName)
        {
            if (!this.Ignores.ContainsKey(type))
            {
                this.Ignores[type] = new HashSet<string>();
            }

            foreach (var prop in propertyName)
            {
                this.Ignores[type].Add(prop);
            }
        }

        public IgnorableSerializerContractResolver Ignore<TModel>(Expression<Func<TModel, object>> selector)
        {
            var body = selector.Body as MemberExpression;

            if (body == null)
            {
                var ubody = (UnaryExpression)selector.Body;
                body = ubody.Operand as MemberExpression;

                if (body == null)
                {
                    throw new ArgumentException("Could not get property name", "selector");
                }
            }

            this.Ignore(typeof(TModel), body.Member.Name);

            return this;
        }

        public bool IsIgnored(Type type, string propertyName)
        {
            if (!this.Ignores.ContainsKey(type))
            {
                return false;
            }

            return this.Ignores[type].Count == 0 ? true : this.Ignores[type].Contains(propertyName);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (this.IsIgnored(property.DeclaringType, property.PropertyName))
            {
                property.ShouldSerialize = instance => { return false; };
            }

            return property;
        }
    }
}
