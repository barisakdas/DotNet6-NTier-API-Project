namespace RepositoryLayer.Seed;
internal class CategorySeed : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(new Category { ID = 1, Name = "Kalemler", CreatedDate = DateTime.Now });
        builder.HasData(new Category { ID = 2, Name = "Kitaplar", CreatedDate = DateTime.Now });
        builder.HasData(new Category { ID = 3, Name = "Defterler", CreatedDate = DateTime.Now });
    }
}


