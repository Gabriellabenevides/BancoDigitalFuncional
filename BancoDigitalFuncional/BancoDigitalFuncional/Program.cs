using System.Diagnostics.CodeAnalysis;
using BancoDigitalFuncional.GraphQL;
using BDFuncional.Domain.Interface;
using BDFuncional.MySql.Repository;
using DBFuncional.Application.Service;
using Microsoft.EntityFrameworkCore;
using MySql;

[ExcludeFromCodeCoverage]
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        var connectionString = builder.Configuration.GetConnectionString("BDFuncionalConnection");
        builder.Services.AddDbContext<MySqlContext>(options =>
        options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)),
        ServiceLifetime.Scoped
        );

        builder.Services.AddGraphQLServer()
            .AddQueryType<BancoDigitalFuncional.GraphQL.Query>()
            .AddMutationType<Mutation>()
            .AddProjections()
            .AddFiltering()
            .AddSorting()
            .AddErrorFilter<CustomErrorFilter>();

        builder.Services.AddScoped<IContaRepository, ContaRepository>();
        builder.Services.AddScoped<IContaService, ContaService>();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<MySqlContext>();
            db.Database.EnsureCreated();

            if (!db.Contas.Any())
            {
                db.Contas.Add(new Domain.Entities.Conta
                {
                    NumeroConta = "123",
                    Saldo = 1000m
                });
                db.SaveChanges();
            }
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();
        app.MapGraphQL("/graphql");
        app.Run();
    }
}
