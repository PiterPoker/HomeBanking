using HomeBanking.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class UserEntityTypeConfiguration
        : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(u => u.Id);

            builder.Ignore(u => u.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("users_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            //Update
            builder
                .Property<DateTime>("Update")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            //RefreshTokenExpiryTime
            builder
                .Property<DateTime?>("RefreshTokenExpiryTime")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired(false);

            //RefreshToken
            builder
                .Property<string>("RefreshToken")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired(false);

            //Password
            builder
                .Property<string>("Password")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            //Login
            builder
                .Property<string>("Login")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasMaxLength(80)
                .IsRequired();

            builder
                .HasOne(u => u.Profile)
                .WithOne(p => p.User)
                .HasForeignKey<Profile>("UserId")
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(b => b.UserRoles)
               .WithOne()
               .HasForeignKey("UserId")
               .OnDelete(DeleteBehavior.Cascade);

            var userRoles = builder.Metadata.FindNavigation(nameof(User.UserRoles));

            userRoles.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
