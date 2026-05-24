using IAM.ConnectorService.Abstractions;
using IAM.ConnectorService.Connectors;
using IAM.ConnectorService.Consumers;
using IAM.Shared.Extensions;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddConsulConfig("ConnectorService");

builder.Services.AddSingleton<IConnector, EntraIDConnector>();

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<ProvisioningRequestedConsumer>();

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/");
        cfg.ReceiveEndpoint("connector-service-queue", e =>
        {
            e.ConfigureConsumer<ProvisioningRequestedConsumer>(context);
        });
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseConsul("ConnectorService");
app.MapControllers();

app.Run();
