using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Adapters.SQL
{
    public class GuestConfiguration : IEntityTypeConfiguration<Guest>
    {
        public void Configure(EntityTypeBuilder<Guest> builder)
        {
            builder.HasKey(x => x.Id);
            builder.OwnsOne( x=>x.DocumentId)
                .Property(x => x.IdNumber)
                .HasColumnName("DocumentNumber")
                .IsRequired();
            builder.OwnsOne(x => x.DocumentId)
                .Property(x => x.DocumentType)
                .HasColumnName("DocumentType")
                .IsRequired();
        }
    }
}
