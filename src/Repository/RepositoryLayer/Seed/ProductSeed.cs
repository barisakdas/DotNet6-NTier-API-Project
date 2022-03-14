namespace RepositoryLayer.Seed;
internal class ProductSeed : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasData(new Product { ID = 1, Name = "Rotring", Stock = 100, Price = 50, CategoryID = 1, CreatedDate = DateTime.Now });
        builder.HasData(new Product { ID = 2, Name = "Faber", Stock = 50, Price = 10, CategoryID = 1, CreatedDate = DateTime.Now });
        builder.HasData(new Product { ID = 3, Name = "Sefiller", Stock = 50, Price = 10, CategoryID = 2, CreatedDate = DateTime.Now });
        builder.HasData(new Product { ID = 4, Name = "Faber", Stock = 50, Price = 10, CategoryID = 2, CreatedDate = DateTime.Now });
        builder.HasData(new Product { ID = 5, Name = "Mopak", Stock = 152, Price = 12, CategoryID = 3, CreatedDate = DateTime.Now });
        builder.HasData(new Product { ID = 6, Name = "Other", Stock = 250, Price = 18, CategoryID = 3, CreatedDate = DateTime.Now });
    }
}

