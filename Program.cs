using Almanime.Repositories;
using Almanime.Services;
using Almanime.Services.Interfaces;
using Almanime.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Nest;
using Serilog;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddEnvironmentVariables();

Log.Logger = new LoggerConfiguration()
  .ReadFrom.Configuration(builder.Configuration)
  .WriteTo.Console()
  .WriteTo.Sentry(options =>
    {
      builder.Configuration.GetSection("Sentry").Bind(options);
    })
  .CreateBootstrapLogger();

Log.Information("Starting up");

try
{
  builder.Host.UseSerilog();
  builder.WebHost.UseSentry();

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
  var elasticSearchUri = builder.Configuration.GetConnectionString("ElasticSearch");
  if (elasticSearchUri != null)
  {
    builder.Services.AddSingleton(new ElasticClient(new Uri(elasticSearchUri)));
  }

  builder.Services.AddCors();

  builder.Services.AddControllers().AddJsonOptions(opts =>
  {
    opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
  });

  // Learn more about configuring Swagger/OpenAlmanime at https://aka.ms/aspnetcore/swashbuckle
  builder.Services.AddEndpointsApiExplorer();
  builder.Services.AddSwaggerGen(setup =>
  {
    // https://stackoverflow.com/a/61365691/10291066
    // https://github.com/domaindrivendev/Swashbuckle.AspNetCore#add-security-definitions-and-requirements
    setup.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
      Name = "Authorization",
      Description = "Please Enter Authentication Token",
      In = ParameterLocation.Header,
      Type = SecuritySchemeType.Http,
      Scheme = "bearer",
      BearerFormat = "JWT"
    });

    setup.OperationFilter<AddAuthHeaderOperationFilter>();
  });

  builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, c =>
    {
      c.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";
      c.TokenValidationParameters = new TokenValidationParameters
      {
        ValidAudience = builder.Configuration["Auth0:Audience"],
        ValidIssuer = builder.Configuration["Auth0:Domain"],
      };
    });

  builder.Services
    .AddAuthorization(options =>
    {
      options.AddPolicy("write:animes", policy => 
        policy.RequireAssertion(context =>
          context.User.HasClaim(claim =>
            claim.Type == "permissions" &&
            claim.Value == "write:animes" &&
            claim.Issuer == $"https://{builder.Configuration["Auth0:Domain"]}/"
          )
        )
      );
      options.AddPolicy("write:episodes", policy => 
        policy.RequireAssertion(context =>
          context.User.HasClaim(claim =>
            claim.Type == "permissions" &&
            claim.Value == "write:episodes" &&
            claim.Issuer == $"https://{builder.Configuration["Auth0:Domain"]}/"
          )
        )
      );
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
catch (HostAbortedException) { }
catch (Exception ex)
{
  Log.Fatal(ex, "Unhandled exception");
}
finally
{
  Log.Information("Shut down complete");
  Log.CloseAndFlush();
}