using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using HomeBanking.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class RelativeEntityTypeConfiguration
        : IEntityTypeConfiguration<Relative>
    {
        public void Configure(EntityTypeBuilder<Relative> builder)
        {
            builder.ToTable("relatives", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("relatives_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            builder
                .Property<int>("_userId")
                //.Property<int>("RelationshipId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("UserId")
                .IsRequired();

            builder
                .Property<int>("_relationshipId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RelationshipId")
                .IsRequired();
        }
    }
}
