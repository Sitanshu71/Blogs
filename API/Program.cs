using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using Sitanshu.Blogs.API.Services;
using Sitanshu.Blogs.Application;
using Sitanshu.Blogs.Application.Common.Interfaces;
using Sitanshu.Blogs.Infrastructure;
using Sitanshu.Blogs.Infrastructure.Persistence;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.RateLimiting;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<ICurrentUserService, CurrentUserService>();
builder.Services.AddTransient<RedisCacheService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddInfrastructureServices(builder.Configuration)
    .AddApplicationServices();

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    rateLimiterOptions.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 10;
        options.Window = TimeSpan.FromSeconds(10);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 5;
    });
});

JwtSecurityTokenHandler.DefaultMapInboundClaims = false;

builder.Services.AddAuthentication("AzureAd")
    .AddMicrosoftIdentityWebApi(builder.Configuration, jwtBearerScheme: "AzureAd")
    .EnableTokenAcquisitionToCallDownstreamApi()
    .AddInMemoryTokenCaches();

builder.Services.AddAuthorization(x =>
{
    x.AddPolicy("Contributor", policy => policy.RequireClaim("roles", "Contributor"));
    x.AddPolicy("Reader", policy => policy.RequireClaim("roles", "Reader"));

    AuthorizationPolicyBuilder authorizationPolicyBuilder = new ("AzureAd");
    authorizationPolicyBuilder = authorizationPolicyBuilder.RequireAuthenticatedUser();

    x.DefaultPolicy = authorizationPolicyBuilder.Build();
    x.FallbackPolicy = authorizationPolicyBuilder.Build();
});

builder.Services.AddSwaggerGen(action =>
{
    var xmlFileName = $"{Assembly.GetEntryAssembly()!.GetName().Name}.xml";
    action.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFileName));

    action.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.OAuth2,
        Name = "oauth2",
        Description = "Microsoft Identity Authentication",
        Flows = new Microsoft.OpenApi.Models.OpenApiOAuthFlows
        {
            Implicit = new Microsoft.OpenApi.Models.OpenApiOAuthFlow
            {
                Scopes = new Dictionary<string, string>
                {
                    { builder.Configuration["AzureAd:Scope"]!, "Application Access" }
                },
                AuthorizationUrl = new Uri($"{builder.Configuration["AzureAd:Instance"]}{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/authorize"),
                TokenUrl = new Uri($"{builder.Configuration["AzureAd:Instance"]}{builder.Configuration["AzureAd:TenantId"]}/oauth2/v2.0/token"),
            }
        }
    });

    action.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            new[] { builder.Configuration["AzureAd:Scope"] }
        }
    });
});

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetSection("RedisConn").Value;
    options.InstanceName = "BlogsCatalog";
});

builder.Host.UseSerilog((ctx, lc) =>
{
    lc.WriteTo.Console();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();
        await initialiser.InitialiseAsync();
        await initialiser.SeedAsync();
    }
}

app.UseRateLimiter();

app.UseSwagger();
app.UseSwaggerUI(action =>
{
    action.OAuthClientId(builder.Configuration["AzureAd:ClientId"]);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();