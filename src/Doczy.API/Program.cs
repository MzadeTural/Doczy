using Doczy.Business.Helpers.Settings;
using Doczy.Business.MappingProfiles;
using Doczy.Business.Services;
using Doczy.DataAccess.Repositories;
using Doczy.DataAccess.Repositories.Implementations;
using Doczy.DataAccess.Repositories.Interfaces;



var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(DoctorMapper));
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IWorkPlaceRepository, WorkPlaceRepository>();
builder.Services.AddBusinessServices();
builder.Services.AddDataAccesServices();
builder.Services.AddRouting();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
