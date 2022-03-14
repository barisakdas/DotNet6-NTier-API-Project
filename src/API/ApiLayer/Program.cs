var builder = WebApplication.CreateBuilder(args);

// Add controllers and filters. Burada kendi yazd���m�z filter � ekliyoruz.
builder.Services.AddControllers(options =>
    options.Filters.Add(new ValidateFilterAttribute()))
        .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

// api nin kendi filter �n� kapatt�k. ��nk� biz endpointten kendi iste�imiz modeli almak istiyoruz.
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();  // MemoryCache ekliyoruz

builder.Services.AddScoped(typeof(NotFoundFilter<>));   // Generic oldu�u i�in typeof ile kullan�yoruz.
builder.Services.AddAutoMapper(typeof(MapProfile));

// Dependency Injection => Bu k�sm� Autofac ile hallettik.
//builder.Services.AddScoped<IUnitOfWorks, UnitOfWork>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

// Kullanaca��m�z db yi �nce gidip appsettings.json i�ine ekledik.
builder.Services.AddDbContext<AppDbContext>(x =>
{
    // appsettings.json dan istedi�im connection stringi alaca��z.
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        // nerede kullan�laca��n� tip g�venli �ekilde belirtiyoruz. Direkt katman�n ismini de yazabilirdik ama bu defa katman ismi de�i�ince buray�da de�i�tirmek zorunda kalaca��m�z i�in yapm�yoruz.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
}
);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());    // Toplu olarak DI kullan�mlar�m�z ve daha fazla yetenek sundu�u i�in buraya AUTOFAC k�t�phanesini ekliyoruz.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder
    => containerBuilder.RegisterModule(new RepositoryServiceModule()));  // Autofac ile yazd���m�z module dosyas�n� eklememiz laz�m.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException(); // Kendi yazd���m�z custom exception middleware.

app.UseAuthorization();

app.MapControllers();   // Bizim controller i�inde yazd���m�z actionlar� yeni halindeki Map ler i�in d�n��t�recek.

app.Run();
