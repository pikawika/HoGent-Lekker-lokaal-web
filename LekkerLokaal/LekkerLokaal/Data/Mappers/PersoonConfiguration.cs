using LekkerLokaal.Models.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LekkerLokaal.Data.Mappers
{
    public class PersoonConfiguration : IEntityTypeConfiguration<Persoon>
    {
        public void Configure(EntityTypeBuilder<Persoon> builder)
        {
            builder.ToTable("Personen");

            builder.HasDiscriminator<string>("persoon_soort")
                .HasValue<Persoon>("persoon_basis")
                .HasValue<Gebruiker>("persoon_gebruiker");
        }
    }
}
