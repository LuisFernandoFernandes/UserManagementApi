using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using UserManagementApi.Authorization;
using UserManagementApi.Data;
using UserManagementApi.Interface;
using UserManagementApi.Interfaces;
using UserManagementApi.Models;
using UserManagementApi.Repositories;
using UserManagementApi.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();

services.AddDbContext<AppDbContext>(options =>
    options.UseInMemoryDatabase("UserManagementInMemoryDB"));

services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


services.AddScoped<IUsuariosService, UsuariosService>();
services.AddScoped<IUsuariosRepository, UsuariosRepository>();

services.AddScoped<IGruposService, GruposService>();
services.AddScoped<IGruposRepository, GruposRepository>();

services.AddSingleton<IAuthorizationHandler, AdministradorHandler>();



builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});


services.AddAuthorization(options =>
{
    options.AddPolicy("ApenasAdministrador", policy =>
    {
        policy.Requirements.Add(new AdministradorRequirement());
    });
});

services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("YsTzB9Jy0tqgNzApAAAAAAAAAAAAYsTzB9Jy0tqgNzApAAAAAAAAAAAAYsTzB9Jy0tqgNzApAAAAAAAAAAAAYsTzB9Jy0tqgNzApAAAAAAAAAAAA")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    dbContext.InitializeAdminUser();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.OAuthClientId("swagger");
        c.OAuthClientSecret("swagger");
        c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


//static void RegisterDependencyInjection(IServiceCollection services)
//{
//    var assembly = Assembly.GetExecutingAssembly();

//    foreach (var type in assembly.GetTypes()
//        .Where(t => t.IsClass && !t.IsAbstract && (t.Name.EndsWith("Service") || t.Name.EndsWith("Repository"))))
//    {
//        var interfaceType = type.GetInterfaces().FirstOrDefault();
//        if (interfaceType != null)
//        {
//            services.AddScoped(interfaceType, type);
//        }
//    }
//}

//public class AddAuthorizationHeaderParameterOperationFilter : IOperationFilter
//{
//    public void Apply(OpenApiOperation operation, OperationFilterContext context)
//    {
//        var filterDescriptor = context.ApiDescription.ActionDescriptor.FilterDescriptors;
//        var isAuthorized = filterDescriptor.Select(filterInfo => filterInfo.Filter).Any(filter => filter is AuthorizeFilter);
//        var allowAnonymous = filterDescriptor.Select(filterInfo => filterInfo.Filter).Any(filter => filter is IAllowAnonymousFilter);

//        if (!isAuthorized || allowAnonymous)
//            return;

//        if (operation.Parameters == null)
//            operation.Parameters = [];

//        operation.Parameters.Add(new OpenApiParameter
//        {
//            Name = "Authorization",
//            Description = "Bearer token",
//            In = ParameterLocation.Header,
//            Required = false,
//            Schema = new OpenApiSchema
//            {
//                Type = "String"
//            }
//        });
//    }
//}