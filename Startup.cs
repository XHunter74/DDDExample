using DDDExample.Extensions;
using DDDExample.Infrastructure;
using DDDExample.Infrastructure.Repositories;
using DDDExample.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using DDDExample.Domain.Events;

namespace DDDExample;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddLogging(e =>
        {
            e.AddSerilog();
        });
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<BankingDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DbConnection")));

        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
        services.AddScoped<AccountService>();
        services.AddScoped<IDomainEventDispatcher, DatabaseDomainEventDispatcher>();
    }

    public void Configure(IApplicationBuilder builder, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            builder.UseSwagger();
            builder.UseSwaggerUI();
        }

        builder.UseAppExceptionHandler();

        builder.UseHttpsRedirection();

        builder.UseRouting();

        builder.UseAuthorization();

        builder.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}