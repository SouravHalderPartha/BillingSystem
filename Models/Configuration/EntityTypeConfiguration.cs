using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BillingSystem.Models.Configuration
{
    public class EntityTypeConfiguration : IEntityTypeConfiguration<OpeningInvoice>
    {
        public void Configure(EntityTypeBuilder<OpeningInvoice> builder)
        {
            builder.Property(b => b.CapacityQty).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(b => b.Rate).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(b => b.BillAmount).HasColumnType("decimal(18,2)").IsRequired();
        }
    }
}
