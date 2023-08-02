using HRApi.BusinessLogic.Contracts;
using HRApi.BusinessLogic.Implementations;
using HRApi.DataAccess;
using HRApi.DataAccess.Contracts;
using HRApi.DataAccess.Implementatios;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<ISalaryIncreaseRepository, SalaryIncreaseRepository>();
builder.Services.AddTransient<ISalaryRevisionRepository, SalaryRevisionRepository>();
builder.Services.AddTransient<IWorkerRepository, WorkerRepository>();

builder.Services.AddTransient<ISalaryRevisionManager, SalaryRevisionManager>();
builder.Services.AddTransient<IWorkerManager, WorkerManager>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<ApplicationContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
