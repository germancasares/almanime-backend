using Almanime.Repositories;
using Almanime.Services;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

    builder.WebHost.UseSentry();

    builder.Configuration.AddEnvironmentVariables();

    builder.Services.AddDbContext<AlmanimeContext>(options =>
    {
        options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("Almanime"));
    });

    // Add services to the container.
    builder.Services.AddScoped<IAnimeService, AnimeService>();
    builder.Services.AddScoped<IEpisodeService, EpisodeService>();
    builder.Services.AddScoped<IUserService, UserService>();
    builder.Services.AddScoped<IFansubService, FansubService>();
    builder.Services.AddScoped<ISubtitleService, SubtitleService>();
    builder.Services.AddScoped<IFileService, FileService>();
    builder.Services.AddScoped<IBookmarkService, BookmarkService>();
    builder.Services.AddScoped<IRoleService, RoleService>();
    builder.Services.AddSingleton<ElasticClient>(
        new ElasticClient(
            new Uri(builder.Configuration.GetConnectionString("ElasticSearch"))
        )
    );

    builder.Host.UseSerilog((context, loggerConfig) => loggerConfig
        .WriteTo.Console()
        .WriteTo.Sentry(options =>
        {
            builder.Configuration.GetSection("Sentry").Bind(options);
        })
    );

    builder.Services.AddCors();

    builder.Services.AddControllers().AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

    // Learn more about configuring Swagger/OpenAlmanime at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(setup =>
    {
        setup.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Bearer",
                Scheme = "oauth2",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
            },
            new List<string>()
        }
    });

        setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Please Enter Authentication Token",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "oauth2",
            Flows = new OpenApiOAuthFlows
            {
                Implicit = new OpenApiOAuthFlow
                {
                    Scopes = new Dictionary<string, string>
                    {
                        { "alm:read_data", "Read data from Almanime" }
                    }
                }
            }
        });
    });

    builder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
        {
            c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
            c.TokenValidationParameters = new TokenValidationParameters
            {
                ValidAudience = builder.Configuration["Auth0:Audience"],
                ValidIssuer = $"{builder.Configuration["Auth0:Domain"]}"
            };
        });

    builder.Services
        .AddAuthorization(o =>
        {
            o.AddPolicy("auth0_new_user", p => p.RequireAuthenticatedUser().RequireClaim("scope", "alm:auth0_new_user"));
        });


    var app = builder.Build();

    app.UseSentryTracing();

    app.UseSerilogRequestLogging();

    // Configure the HTTP request pipeline.
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


    app.UseMiddleware<ErrorHandlerMiddleware>();

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