namespace ApiLayer.Modules;
public class RepositoryServiceModule : Autofac.Module   // Burada hem Reflection hemde Autofac trafında Module class ı var. Dikkat etmemiz lazım.   
{
    protected override void Load(ContainerBuilder builder)  // Burada eklenenlerin tekrar program.cs içinde eklenmesine gerek yok. (.Net5 te startup)
    {
        // Generic olanları ekliyoruz
        builder.RegisterGeneric(typeof(GenericRepository<>))
            .As(typeof(IGenericRepository<>))
            .InstancePerLifetimeScope();
        builder.RegisterGeneric(typeof(Service<>))
            .As(typeof(IService<>))
            .InstancePerLifetimeScope();

        builder.RegisterType<UnitOfWork>().As<IUnitOfWorks>();

        // Generic olmayanları eklemek için önce assemblyleri alıyoruz.
        var apiAssembly = Assembly.GetExecutingAssembly();// Üzerinde çalıştığımız assembly yi alıyoruz.
        var repoAssembly = Assembly.GetAssembly(typeof(AppDbContext)); // Data katmanındaki assembly için o katmandaki bir uygulamayı veriyoruz.
        var serviceAssembly = Assembly.GetAssembly(typeof(Service<>)); // Service katmanındaki assembly için o katmandaki bir uygulamayı veriyoruz.

        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Repository"))  // Sonu repository ile bitenleri al.
            .AsImplementedInterfaces()  // Alınan repository lerin interface lerini implemente et.
            .InstancePerLifetimeScope();    // Autofac ta scope a karşılık geliyor.

        builder.RegisterAssemblyTypes(apiAssembly, repoAssembly, serviceAssembly)
            .Where(x => x.Name.EndsWith("Service"))  // Sonu service ile bitenleri al.
            .AsImplementedInterfaces()  // Alınan repository lerin interface lerini implemente et.
            .InstancePerLifetimeScope();    // Autofac ta scope a karşılık geliyor.

        builder.RegisterType<ProductServiceWithCache>().As<IProductService>();  // Cache yapısını ekledik. Bunun eklenmesi için ana servisin isimini değiştirdik.


    }
}

