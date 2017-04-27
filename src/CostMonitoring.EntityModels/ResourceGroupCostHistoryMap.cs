using System.Data.Entity.ModelConfiguration;

namespace CostMonitoring.EntityModels
{
    /// <summary>
    /// This represents the mapping entity for the <see cref="ResourceGroupCostHistory"/> class.
    /// </summary>
    public class ResourceGroupCostHistoryMap : EntityTypeConfiguration<ResourceGroupCostHistory>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceGroupCostHistoryMap"/> class.
        /// </summary>
        public ResourceGroupCostHistoryMap()
        {
            // Primary Key
            this.HasKey(p => p.ResourceGroupCostHistoryId);

            // Properties
            this.Property(p => p.ResourceGroupCostHistoryId).IsRequired();
            this.Property(p => p.Subscription).IsRequired().HasMaxLength(128);
            this.Property(p => p.SubscriptionId).IsRequired();
            this.Property(p => p.ResourceGroupName).IsOptional().HasMaxLength(128);
            this.Property(p => p.Owners).IsOptional().HasMaxLength(512);
            this.Property(p => p.Cost).IsRequired();
            this.Property(p => p.TotalSpendLimit).IsOptional();
            this.Property(p => p.DailySpendLimit).IsOptional();
            this.Property(p => p.OverspendAction).IsOptional().HasMaxLength(16);
            this.Property(p => p.DateStart).IsRequired();
            this.Property(p => p.DateEnd).IsRequired();
            this.Property(p => p.DateCreated).IsRequired();
            this.Property(p => p.DateUpdated).IsRequired();

            // Table & Column Mappings
            this.ToTable("ResourceGroupCostHistory");
            this.Property(p => p.ResourceGroupCostHistoryId).HasColumnName("ResourceGroupCostHistoryId");
            this.Property(p => p.Subscription).HasColumnName("Subscription");
            this.Property(p => p.SubscriptionId).HasColumnName("SubscriptionId");
            this.Property(p => p.ResourceGroupName).HasColumnName("ResourceGroupName");
            this.Property(p => p.Owners).HasColumnName("Owners");
            this.Property(p => p.Cost).HasColumnName("Cost");
            this.Property(p => p.TotalSpendLimit).HasColumnName("TotalSpendLimit");
            this.Property(p => p.DailySpendLimit).HasColumnName("DailySpendLimit");
            this.Property(p => p.OverspendAction).HasColumnName("OverspendAction");
            this.Property(p => p.DateStart).HasColumnName("DateStart");
            this.Property(p => p.DateEnd).HasColumnName("DateEnd");
            this.Property(p => p.DateCreated).HasColumnName("DateCreated");
            this.Property(p => p.DateUpdated).HasColumnName("DateUpdated");

            // Relationships
        }
    }
}