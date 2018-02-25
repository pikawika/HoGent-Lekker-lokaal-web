using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using LekkerLokaal.Models.Domain;

namespace LekkerLokaal.Data.Mappers
{
    internal class BestelLijnConfiguration : IEntityTypeConfiguration<BestelLijn>
    {
        public void Configure(EntityTypeBuilder<BestelLijn> builder)
        {
            builder.ToTable("BestelLijn");
            builder.HasKey(t => new { t.BestellingId, t.ProductId });

            builder.HasOne(t => t.Bon).WithMany().IsRequired().HasForeignKey(t => t.ProductId).OnDelete(DeleteBehavior.Restrict);
        }
    }
}
