using HomeBanking.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class UserRoleEntityTypeConfiguration
        : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.ToTable("userroles", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("userroles_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            builder
                .Property<int>("_roleId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RoleId")
                .IsRequired();
        }
    }
}
