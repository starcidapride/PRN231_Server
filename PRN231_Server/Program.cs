using BussinessObjects;
using Microsoft.AspNetCore.OData;
using Repositories;
using Repositories.Impl;
using Service;
using Service.Impl;
using Service.Model;

var builder = WebApplication.CreateBuilder(args);

//Add mapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Add services to the container.
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService,UserService>();

//add Cors
builder.Services.AddCors();

//add odata
builder.Services.AddControllers().AddOData(
    options=>options.SetMaxTop(100).Filter().Expand().Select().Expand()
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();

app.UseSwaggerUI();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
