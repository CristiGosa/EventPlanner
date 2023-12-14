using EventPlanner.Authentication;
using EventPlanner.Database.Context;
using EventPlanner.Domain.Entities;
using EventPlanner.Api.Extensions;
using EventPlanner.Business;
using EventPlanner.Database;

using MicroElements.Swashbuckle.FluentValidation.AspNetCore;

using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var eventPlannerOrigin = "EventPlannerOrigin";

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureBusiness(builder.Configuration)
    .ConfigurePersistence(builder.Configuration);

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<DataContext>();

builder.Services.ConfigureAuthentication(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: eventPlannerOrigin,
        policy =>
        {
            policy
            .WithOrigins(builder.Configuration.GetSection("AllowedOrigins").Get<string[]>())
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Description = "JWT Authorization header using the Bearer scheme."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
             new OpenApiSecurityScheme
             {
                 Reference = new OpenApiReference
                 {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                 }
             },
            new string[] { }
         }
     });
});

WebApplication app = builder.Build();

app.UseCors(eventPlannerOrigin);

if (app.Environment.IsDevelopment())
{
    _ = app.UseSwagger();
    _ = app.UseSwaggerUI();
}

app.UseErrorHandler();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
