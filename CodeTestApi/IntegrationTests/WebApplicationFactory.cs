﻿using Mongo2Go;
using MongoDB.Driver;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace IntegrationTests;
public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
{
    private MongoDbRunner _mongoDbRunner;

    protected override IHost CreateHost(IHostBuilder builder)
    {
        _mongoDbRunner = MongoDbRunner.Start();  // Iniciar Mongo2Go

        builder.ConfigureServices(services =>
        {
            // Eliminar la configuración original de MongoDB
            var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IMongoClient));
            if (descriptor != null)
            {
                services.Remove(descriptor);
            }

            // Inyectar Mongo2Go como el MongoClient para pruebas
            services.AddSingleton<IMongoClient, MongoClient>(sp =>
            {
                return new MongoClient(_mongoDbRunner.ConnectionString);
            });

            services.AddScoped<IMongoDatabase>(sp =>
            {
                var client = sp.GetRequiredService<IMongoClient>();
                return client.GetDatabase("TestDb");  // Usa una base de datos de prueba
            });
        });

        return base.CreateHost(builder);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _mongoDbRunner?.Dispose();  // Detener Mongo2Go cuando terminen las pruebas
    }
}
