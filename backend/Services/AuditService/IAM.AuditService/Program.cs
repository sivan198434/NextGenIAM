using IAM.AuditService.Data;
using IAM.AuditService.Services;
using IAM.AuditService.Consumers;
using IAM.Shared.Extensions;
using MassTransit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AuditDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<AuditStore>();

builder.Services.AddConsulConfig("AuditService");

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<AuditEventConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/");
        cfg.ReceiveEndpoint("audit-service-queue", e =>
        {
            e.ConfigureConsumer<AuditEventConsumer>(context);
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseConsul("AuditService");

app.MapControllers();

app.Run();
