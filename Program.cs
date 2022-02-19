using Almanime.Repositories;
using Almanime.Services;
using Almanime.Services.Interfaces;
using IdentityServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Nest;
using Serilog;
using System.Text.Json.Serialization;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration.AddEnvironmentVariables();

    builder.Host.UseSerilog((ctx, lc) => lc
        .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
        .Enrich.FromLogContext()
        .ReadFrom.Configuration(ctx.Configuration));

    builder.Services.AddDbContext<AlmanimeContext>(options =>
    {
        options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("Almanime"));
    });

    builder.Services.AddScoped<IAnimeService, AnimeService>();
    builder.Services.AddScoped<IEpisodeService, EpisodeService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IFansubService, FansubService>();
    builder.Services.AddScoped<ISubtitleService, SubtitleService>();
    builder.Services.AddScoped<IFileService, FileService>();
    builder.Services.AddScoped<IBookmarkService, BookmarkService>();
    builder.Services.AddSingleton<ElasticClient>(
        new ElasticClient(
            new Uri(builder.Configuration.GetConnectionString("ElasticSearch"))
        )
    );

    builder.Services.AddCors();

    builder.Services.AddControllers().AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
        {
            options.Authority = "https://localhost:5001";

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false
            };
        });

    builder.Services
        .AddAuthorization(o =>
        {
            o.AddPolicy("auth0_new_user", p => p.RequireAuthenticatedUser().RequireClaim("scope", "alm:auth0_new_user"));
        });

    builder.Services
        .AddIdentityServer()
        .AddInMemoryApiScopes(Config.ApiScopes)
        .AddInMemoryClients(Config.Clients);

    var app = builder.Build();

    app.UseSerilogRequestLogging();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    else
    {
        using var scope = app.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<AlmanimeContext>();
        db.Database.Migrate();
    }

    app.UseIdentityServer();

    app.UseHttpsRedirection();

    app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information("Shut down complete");
    Log.CloseAndFlush();
}