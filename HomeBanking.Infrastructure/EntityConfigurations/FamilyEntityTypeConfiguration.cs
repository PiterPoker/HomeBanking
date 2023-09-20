using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class FamilyEntityTypeConfiguration
        : IEntityTypeConfiguration<Family>
    {
        public void Configure(EntityTypeBuilder<Family> builder)
        {
            builder.ToTable("families", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("families_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            //Name
            builder
                .Property<string>("Name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder
                .HasMany(b => b.Invitations)
               .WithOne()
               .HasForeignKey("FamilyId")
               .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(b => b.Relatives)
               .WithOne()
               .HasForeignKey("FamilyId")
               .OnDelete(DeleteBehavior.Cascade);

            var invitations = builder.Metadata.FindNavigation(nameof(Family.Invitations));
            var relatives = builder.Metadata.FindNavigation(nameof(Family.Relatives));

            invitations.SetPropertyAccessMode(PropertyAccessMode.Field);
            relatives.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
