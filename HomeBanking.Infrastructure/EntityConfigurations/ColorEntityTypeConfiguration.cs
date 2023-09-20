using HomeBanking.Domain.AggregatesModel.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class ColorEntityTypeConfiguration
        : IEntityTypeConfiguration<Color>
    {
        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.ToTable("colors", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder
                .Property<int>("id")
                .HasDefaultValue(1)
                .ValueGeneratedNever()
                .IsRequired();

            builder.Property(o => o.Name)
                .HasMaxLength(200)
                .IsRequired();
        }
    }
}
