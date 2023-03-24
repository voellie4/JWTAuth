using JWTAuth.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(setup =>
{
    setup.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:44417")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .SetIsOriginAllowed((host) => true) //important! to make angular api call works
          .AllowCredentials();
    });
});

builder.Services.AddControllersWithViews();

// Configure JWT authentication
var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .Build();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = configuration.GetValue<string>("JWT:Issuer"),
            ValidAudience = configuration.GetValue<string>("JWT:Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("JWT:Secret")))
        };
    });

// Configure policies for authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy =>
        policy.RequireAuthenticatedUser().RequireRole("Admin"));
    options.AddPolicy("ManagerOrAdmin", policy =>
        policy.RequireAuthenticatedUser().RequireRole("Manager", "Admin"));
    options.AddPolicy("AuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser());
    options.AddPolicy("MinimumAgePolicy", policy =>
        policy.Requirements.Add(new MinimumAgeRequirement(18)));
});

builder.Services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html"); ;

app.Run();
