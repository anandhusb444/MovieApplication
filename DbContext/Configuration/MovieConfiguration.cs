using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MovieApplication.Models;

namespace MovieApplication.DbContext.Configuration
{
    public class MovieConfiguration : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            builder.ToTable("Movies");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.Genre)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.ReleseDate)
                .IsRequired();

            builder.Property(p => p.Rating)
                .IsRequired();

            builder.Property(p => p.Created)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(p => p.LastModifed)
                .IsRequired()
                .ValueGeneratedOnUpdate();

            builder.HasIndex(p => p.Title);
        }

    }
}
