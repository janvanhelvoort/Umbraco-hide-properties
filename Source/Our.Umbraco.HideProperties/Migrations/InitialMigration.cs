namespace Our.Umbraco.HideProperties.Migrations
{
    using global::Umbraco.Core;
    using global::Umbraco.Core.Logging;
    using global::Umbraco.Core.Persistence;
    using global::Umbraco.Core.Persistence.Migrations;
    using global::Umbraco.Core.Persistence.SqlSyntax;

    using Our.Umbraco.HideProperties.Constants;
    using Our.Umbraco.HideProperties.Models.Pocos;

    [Migration("0.1.0", 1, ApplicationConstants.ProductName)]
    public class InitialMigration : MigrationBase
    {
        private readonly UmbracoDatabase database = ApplicationContext.Current.DatabaseContext.Database;
        private readonly DatabaseSchemaHelper databaseSchema;

        public InitialMigration(ISqlSyntaxProvider sqlSyntax, ILogger logger) : base(sqlSyntax, logger)
        {
            this.databaseSchema = new DatabaseSchemaHelper(this.database, logger, sqlSyntax);
        }

        public override void Up()
        {
            if (!this.databaseSchema.TableExist(TableConstants.Rules.TableName))
            {
                this.Create.Table(TableConstants.Rules.TableName)
                .WithColumn("Id").AsInt16().NotNullable()
                    .PrimaryKey("PK_rules").Identity()
                .WithColumn("Key").AsGuid().NotNullable()
                .WithColumn("IsActive").AsBoolean().NotNullable()
                .WithColumn("ContentTypeAlias").AsString().NotNullable()
                .WithColumn("Tabs").AsString().Nullable()
                .WithColumn("Properties").AsString().Nullable()
                .WithColumn("UserGroups").AsString().Nullable()
                .WithColumn("IsDeleted").AsBoolean().NotNullable();
            }
        }

        public override void Down()
        {
            this.databaseSchema.DropTable<Rule>();
        }
    }
}
