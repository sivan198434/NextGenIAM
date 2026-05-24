using IAM.Shared.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddConsulConfig("Gateway");

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

var app = builder.Build();

app.MapReverseProxy();

app.Run();
