using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShope.Core.Entities.Security;

namespace OnlineShope.Core.FluentAPIConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(s => s.Id);
            builder.Property(p => p.UserName)
                .IsRequired()
                .HasMaxLength(64);
        }
    }

}
