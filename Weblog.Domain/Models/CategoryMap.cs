using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Weblog.Domain.Models
{  
    

    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Categories");

            builder.Property(x => x.Order);
            builder.Property(x => x.Articles);
            builder.Property(x => x.Children);
            builder.Property(x => x.Title);

            builder.HasMany(x => x.Children).WithOne().IsRequired(false).HasForeignKey(x => x.ParentId);
        }
    }
}