using Core.Repositories;
using Core.Services;
using Core.UnýtOfWorks;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Repositories.UnýtOfWork;
using Repositories.Repositories;
using System.Reflection;
using Services.Services;
using Services.Mapping;
using Autofac.Extensions.DependencyInjection;
using Autofac;
using FluentValidation.AspNetCore;
using Services.Validations;
using API.Filters;
using Microsoft.AspNetCore.Mvc;
using API.Midllewares;
using API.Modules;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options=> options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x=>x.RegisterValidatorsFromAssemblyContaining<ProductDtoValidator>());

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddAutoMapper(typeof(MapProfile));


//builder.Services.AddScoped<IUnýtOfWork, UnýtOfWork>();
//builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
//builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));

//builder.Services.AddScoped<IProductRepository, ProductRepository>();
//builder.Services.AddScoped<IProductService, ProductService>();

//builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
//builder.Services.AddScoped<ICategoryService, CategoryService>();


builder.Services.AddDbContext<AppDbContext>(x =>
{

    x.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection"), option =>
    {
    option.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDbContext)).GetName().Name);

    });
});


    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

    builder.Host.ConfigureContainer<ContainerBuilder>(ContainerBuilder => ContainerBuilder.RegisterModule(new RepoServiceModule()));


    var app = builder.Build();


    if (app.Environment.IsDevelopment())
{
        app.UseSwagger();
        app.UseSwaggerUI();
}

    app.UseHttpsRedirection();

    app.UseCustomException();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
