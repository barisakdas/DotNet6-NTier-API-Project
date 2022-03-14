namespace RepositoryLayer.Seed
{
    internal class ProductFeatureSeed : IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(new ProductFeature { ID = 1, Color = "Kırmızı", ProductID = 1 });
            builder.HasData(new ProductFeature { ID = 2, Color = "Mavi", ProductID = 2 });
        }
    }
}
