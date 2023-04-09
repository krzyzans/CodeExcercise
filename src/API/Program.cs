using CodeExcercise.Common.Configuration.Extensions;
using CodeExcercise.Common.Middleware;
using CodeExcercise.Database.Context;
using CodeExcercise.Services;
using CodeExcercise.Services.Extensions;

namespace CodeExcercise;

/// <summary>
/// Main method to start app
/// </summary>
public class Program
{
    /// <summary>
    /// Main method
    /// </summary>
    /// <param name="args">Arguments from external</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddSqlConfiguration(builder.Configuration);
        builder.Services.AddServices();

        //TODO add logger context
        builder.Services.AddLogging();

        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

        builder.Services.AddDbContext<CustomerContext>();

        var app = builder.Build();

        //error middleware
        app.UseMiddleware<ErrorHandlingMiddleware>();

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
    }
}
