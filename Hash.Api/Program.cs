using AutoMapper;
using Hash.Api.Configurations;
using Hash.DAL.Context;
using Hash.DAL.Repositories.Implementations;
using Hash.DAL.Repositories.Interfaces;
using Hash.Service.Implementations;
using Hash.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

builder.Services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
builder.Services.AddScoped(typeof(IHashRepository), typeof(HashRepository));

builder.Services.AddScoped(typeof(IEntityService<>), typeof(EntityService<>));

builder.Services.AddScoped<IHashService, HashService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
