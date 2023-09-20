using HomeBanking.Domain.AggregatesModel.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class CategoryEntityTypeConfiguration
        : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("categories", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("categories_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            //Name
            builder
                .Property<string>("_name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("Name")
                .IsRequired();

            //ColorId
            builder
                .Property<int>("_colorId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("ColorId")
                .IsRequired();
        }
    }
}
