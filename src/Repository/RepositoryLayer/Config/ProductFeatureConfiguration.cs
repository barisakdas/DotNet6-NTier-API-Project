namespace RepositoryLayer.Config;
internal class ProductFeatureConfiguration : IEntityTypeConfiguration<ProductFeature>
{
    public void Configure(EntityTypeBuilder<ProductFeature> builder)
    {
        builder.HasKey(x => x.ID);

        builder.Property(x => x.ID)
            .UseIdentityColumn();

        builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature).HasForeignKey<ProductFeature>(x => x.ProductID);
    }
}

