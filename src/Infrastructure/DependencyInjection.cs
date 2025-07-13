using Application.Common;
using Application.Common.Interfaces;
using Application.Common.Interfaces.Repositories;
using Application.Common.Interfaces.UnitOfWork;
using Ardalis.GuardClauses;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.SignalR;
using Infrastructure.UOWork;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IHostApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
              ?? Environment.GetEnvironmentVariable("DefaultConnection");
            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });
            Console.WriteLine($"--- DATABASE CONNECTION ATTEMPT ---");
            Console.WriteLine($"Using connection string: {connectionString}");
            Console.WriteLine($"--- END DATABASE CONNECTION ---");

            builder.Services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<INotificationRepository, NotificationRepository>();
            builder.Services.AddSingleton<IUserIdProvider, CustomUserIdProvider>();

            builder.Services.AddMassTransit(x =>
            {
                // 1. Scan the current assembly for any classes that implement IConsumer
                // This will find any consumers you create in the Infrastructure project.
                x.AddConsumers(System.Reflection.Assembly.GetExecutingAssembly());

                // 2. Configure MassTransit to use RabbitMQ as the transport
                x.UsingRabbitMq((context, cfg) =>
                {
                    // 3. Read the host, user, and password from configuration/environment variables
                    var host = builder.Configuration["RABBITMQ_HOST"] ?? "localhost";
                    var user = builder.Configuration["RABBITMQ_USER"] ?? "guest";
                    var password = builder.Configuration["RABBITMQ_PASS"] ?? "guest";

                    // 4. Set the host connection details
                    cfg.Host(host, "/", h =>
                    {
                        h.Username(user);
                        h.Password(password);
                    });


                    // 5. This is the magic! It automatically configures a receive endpoint (a queue)
                    // for each consumer that was discovered in step 1.
                    cfg.ConfigureEndpoints(context);
                });
            });
        }
    }
}
