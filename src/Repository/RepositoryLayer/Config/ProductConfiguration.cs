namespace RepositoryLayer.Config;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(x => x.ID);

        builder.Property(x => x.ID)
            .UseIdentityColumn();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Stock)
            .IsRequired();

        builder.Property(x => x.Price)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Category).WithMany(x => x.Products).HasForeignKey(x => x.CategoryID); // Bunu EFCore algılayacak zaten ama yinede yazalım.
    }

}
