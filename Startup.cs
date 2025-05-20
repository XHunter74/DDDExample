using DDDExample.Infrastructure;
using DDDExample.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

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
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddDbContext<BankingDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DbConnection")));

        services.AddScoped<IBankAccountRepository, BankAccountRepository>();
    }

    public void Configure(IApplicationBuilder builder, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            builder.UseSwagger();
            builder.UseSwaggerUI();
        }

        builder.UseHttpsRedirection();

        builder.UseRouting();

        builder.UseAuthorization();

        builder.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}