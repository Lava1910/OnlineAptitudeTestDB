using Microsoft.EntityFrameworkCore;
using OnlineAptitudeTestDB.Entities;
using OnlineAptitudeTestDB.Interfaces;
using OnlineAptitudeTestDB.Service;

var builder = WebApplication.CreateBuilder(args);

// add Cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin();
            policy.AllowAnyMethod();
            policy.AllowAnyHeader();
        }
        );
}
);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("OnlineAptitudeTestDB");
builder.Services.AddDbContext<OnlineAptitudeTestDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddScoped<IManageCandidateService, ManageCandidateService>();
builder.Services.AddScoped<IManageQuestionService, ManageQuestionService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
