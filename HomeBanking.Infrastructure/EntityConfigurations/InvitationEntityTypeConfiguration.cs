using HomeBanking.Domain.AggregatesModel.FamilyAggregate;
using HomeBanking.Domain.AggregatesModel.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeBanking.Infrastructure.EntityConfigurations
{
    internal class InvitationEntityTypeConfiguration
        : IEntityTypeConfiguration<Invitation>
    {
        public void Configure(EntityTypeBuilder<Invitation> builder)
        {
            builder.ToTable("invitations", HomeBankingContext.DEFAULT_SCHEMA);

            builder.HasKey(o => o.Id);

            builder.Ignore(b => b.DomainEvents);

            builder.Property(u => u.Id)
                .UseHiLo("invitations_Id_seq", HomeBankingContext.DEFAULT_SCHEMA);

            builder
                .Property<string>("Comment")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired(false);

            builder
                .Property<bool>("IsActual")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder
                .Property<int>("_statusId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("StatusId")
                .IsRequired();

            builder
                .Property<int>("_relationshipId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("RelationshipId")
                .IsRequired();

            builder
                .Property<int>("FromId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder
                .Property<int>("ToId")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey("FromId")
                .IsRequired();

            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey("ToId")
                .IsRequired();
        }
    }
}
