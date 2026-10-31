using Microsoft.EntityFrameworkCore;
using PayrollApis.CrudService;
using PayrollApis.Models;
using PayrollApis.ModelServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var provider=builder.Services.BuildServiceProvider();
var config=provider.GetRequiredService<IConfiguration>();
builder.Services.AddDbContext<SqlDbContext>(option => option.UseSqlServer(config.GetConnectionString("dbcs")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<EmployeeServices>();
builder.Services.AddScoped<DepartmentServices>();
builder.Services.AddScoped<DesignationServices>();
builder.Services.AddScoped<EmployeeOffDetailServices>();
builder.Services.AddScoped<EmployeeBankDetailServices>();
builder.Services.AddScoped<EmployeeStatuaryServices>();
builder.Services.AddScoped<EmployeeSalaryStructureServices>();
builder.Services.AddScoped<EmployeeSalaryTransactionServices>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngular");
app.UseAuthorization(); 

app.MapControllers();

app.Run();
