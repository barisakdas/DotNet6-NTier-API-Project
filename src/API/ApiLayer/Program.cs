var builder = WebApplication.CreateBuilder(args);

// Add controllers and filters. Burada kendi yazdýðýmýz filter ý ekliyoruz.
builder.Services.AddControllers(options =>
    options.Filters.Add(new ValidateFilterAttribute()))
        .AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

// api nin kendi filter ýný kapattýk. Çünkü biz endpointten kendi isteðimiz modeli almak istiyoruz.
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMemoryCache();  // MemoryCache ekliyoruz

builder.Services.AddScoped(typeof(NotFoundFilter<>));   // Generic olduðu için typeof ile kullanýyoruz.
builder.Services.AddAutoMapper(typeof(MapProfile));

// Dependency Injection => Bu kýsmý Autofac ile hallettik.
//builder.Services.AddScoped<IUnitOfWorks, UnitOfWork>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();
//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();
//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

// Kullanacaðýmýz db yi önce gidip appsettings.json içine ekledik.
builder.Services.AddDbContext<AppDbContext>(x =>
{
    // appsettings.json dan istediðim connection stringi alacaðýz.
    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
        // nerede kullanýlacaðýný tip güvenli þekilde belirtiyoruz. Direkt katmanýn ismini de yazabilirdik ama bu defa katman ismi deðiþince burayýda deðiþtirmek zorunda kalacaðýmýz için yapmýyoruz.
        option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);
    });
}
);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());    // Toplu olarak DI kullanýmlarýmýz ve daha fazla yetenek sunduðu için buraya AUTOFAC kütüphanesini ekliyoruz.
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder
    => containerBuilder.RegisterModule(new RepositoryServiceModule()));  // Autofac ile yazdýðýmýz module dosyasýný eklememiz lazým.

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCustomException(); // Kendi yazdýðýmýz custom exception middleware.

app.UseAuthorization();

app.MapControllers();   // Bizim controller içinde yazdýðýmýz actionlarý yeni halindeki Map ler için dönüþtürecek.

app.Run();
