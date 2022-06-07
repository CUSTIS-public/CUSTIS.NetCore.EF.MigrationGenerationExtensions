using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TestDataAccessLayer
{
    public class Class1Configuration : IEntityTypeConfiguration<Class1>
    {
        public void Configure(EntityTypeBuilder<Class1> builder)
        {
            builder.ToTable("my_table").HasKey(e => e.Id);
        }
    }
}