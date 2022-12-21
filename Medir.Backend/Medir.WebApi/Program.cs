using Microsoft.EntityFrameworkCore;
using Medir.Application;
using Medir.Application.Common.Mappings;
using Medir.Application.Interfaces;
using Medir.Persistence;
using Medir.WebApi.Middleware;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using Medir.WebApi.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Medir.WebApi.Repository;
using Microsoft.IdentityModel.Tokens;
using Medir.WebApi.JwtFeatures;
using System.Text;
using Medir.WebApi.Extensions;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Http.Features;
using EmailService;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureNpgContext(builder.Configuration);
builder.Services.AddAutoMapper(con =>
{
    con.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    con.AddProfile(new AssemblyMappingProfile(typeof(IMedirDbContext).Assembly));
});

builder.Services.AddDbContext<MedirDbContext>(options =>
    options.UseNpgsql(
            builder.Configuration
                .GetConnectionString(MedirDbContext.ConnectionString)));

builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.Password.RequiredLength = 7;
    opt.Password.RequireNonAlphanumeric = false;

    opt.User.RequireUniqueEmail = true;

}).AddEntityFrameworkStores<RepositoryContext>()
    .AddDefaultTokenProviders()
    .AddRussianIdentityErrorDescriber();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

var jwtSettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

})    
    .AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["validIssuer"],
        ValidAudience = jwtSettings["validAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});

builder.Services.AddScoped<JwtHandler>();

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});

var emailConfig = builder.Configuration
        .GetSection("EmailConfiguration")
        .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailSender, EmailSender>();

builder.Services.AddControllers();

builder.Services.AddApplication();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddOpenApiDocument();



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var serviceProvider = scope.ServiceProvider;
        try
        {
            var dbService = serviceProvider.GetRequiredService<MedirDbContext>();
            DbInitializer.Initialize(dbService);
            var idbService = serviceProvider.GetRequiredService<RepositoryContext>();
            DbIdentityInitializer.Initialize(idbService);

            await RoleInitializer.InitializeAsync(serviceProvider.GetRequiredService<UserManager<User>>(),
                serviceProvider.GetRequiredService<RoleManager<IdentityRole>>());
        }
        catch (Exception exception)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(exception, "An error occured while app initialization");
        }
    }
}

app.UseOpenApi();
app.UseSwaggerUi3();

app.UseCustomExceptionHandler();

app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Resources")),
    RequestPath = new PathString("/Resources")
});

app.UseCookiePolicy();

app.UseRouting();

app.UseCors("AllowAll");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

