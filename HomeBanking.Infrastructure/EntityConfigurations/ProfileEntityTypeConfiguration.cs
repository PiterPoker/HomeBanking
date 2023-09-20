using HomeBanking.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class ProfileEntityTypeConfiguration
        : IEntityTypeConfiguration<Profile>
    {
        public void Configure(EntityTypeBuilder<Profile> builder)
        {
            builder.ToTable("profiles", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(u => u.Id);

            builder.Ignore(u => u.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("profiles_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            //Name
            builder
                .Property<string>("Name")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            //BirthDay
            builder
                .Property<DateTime>("Birthday")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            //NumberPhone
            builder
                .Property<string>("PhoneNumber")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            //UserId
            builder
                .Property<int?>("UserId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired(false);
        }
    }
}
