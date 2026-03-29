using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Ordering.Application.Abstractions;
using Ordering.Application.Behaviors;
using Ordering.Application.Validators;
using Ordering.Core.Repositories;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Repositories;

namespace Ordering.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOrderingServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Add data base context
            services.AddDbContext<OrderContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("OrderingConnectionString"), sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        maxRetryCount: 5,
                        maxRetryDelay: TimeSpan.FromSeconds(10),
                        errorNumbersToAdd: null);
                }));
            // Add Repositories 
            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            services.AddScoped<IOrderRepository, OrderRepository>();
            // Add CQRS
            services.Scan(scan => scan.FromAssemblies(
                typeof(ICommandHandler<>).Assembly)
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(c => c.AssignableTo(typeof(ICommandHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
                .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime()
            );
            //FluentValidation
            services.AddValidatorsFromAssembly(typeof(CreateOrderCommandValidator).Assembly);
            //Decorators pipline
            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(ValidationCommandHandlerDecorator<,>)
                );
            services.Decorate(
                typeof(ICommandHandler<,>),
                typeof(UnhandleExceptionCommandHandlerDecorator<,>)
                );

            return services;
        }
    }
}
