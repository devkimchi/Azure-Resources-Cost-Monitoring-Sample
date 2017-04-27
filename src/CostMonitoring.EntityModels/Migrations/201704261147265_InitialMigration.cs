namespace CostMonitoring.EntityModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResourceGroupCostHistory",
                c => new
                    {
                        ResourceGroupCostHistoryId = c.Guid(nullable: false),
                        Subscription = c.String(nullable: false, maxLength: 128),
                        SubscriptionId = c.Guid(nullable: false),
                        ResourceGroupName = c.String(maxLength: 128),
                        Owners = c.String(maxLength: 512),
                        Cost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        TotalSpendLimit = c.Decimal(precision: 18, scale: 2),
                        DailySpendLimit = c.Decimal(precision: 18, scale: 2),
                        OverspendAction = c.String(maxLength: 16),
                        DateStart = c.DateTimeOffset(nullable: false, precision: 7),
                        DateEnd = c.DateTimeOffset(nullable: false, precision: 7),
                        DateCreated = c.DateTimeOffset(nullable: false, precision: 7),
                        DateUpdated = c.DateTimeOffset(nullable: false, precision: 7),
                    })
                .PrimaryKey(t => t.ResourceGroupCostHistoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ResourceGroupCostHistory");
        }
    }
}
