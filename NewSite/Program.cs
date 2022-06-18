using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using NewSite;
using NewSite.Helper_s.Impl;
using NewSite.Helper_s.Interface;
using NewSite.Repository.Abstraction;
using NewSite.Repository.Impl;
using NewSite.Service.Impl;
using NewSite.Service.Interface;
using System.Text;
using NewSite.Service.Impl.Profile;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NewSite", Version = "v1" });
});

builder.Services.AddSession(option =>
{
    option.IdleTimeout = System.TimeSpan.FromMinutes(15);
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
            .AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<NewSiteContext>();
#region DI
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
#endregion
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(typeof(UserProfile));
#region JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, x =>
    {
        x.TokenValidationParameters = new TokenValidationParameters
        {
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
#endregion

var app = builder.Build();


app.UseHttpsRedirection();
app.UseSession();
app.UseCors();
app.UseRouting();

app.Use(async (context, next) =>
{
    var token = context.Session.GetString("Token");
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers.Add("Authorization", "Bearer " + token);
    }
    await next();
});

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

    app.UseSwagger(options =>
    {
        options.RouteTemplate = "newsite/swagger/{documentname}/swagger.json";
        if (!app.Environment.IsDevelopment())
        {
            options.PreSerializeFilters.Add((swagger, httpReq) =>
                swagger.Servers = new List<OpenApiServer>
                    {new OpenApiServer {Url = $"{httpReq.Scheme}s://{httpReq.Host.Value}/newsite"}});
        }
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/newsite/swagger/v1/swagger.json", "API V1");
        options.RoutePrefix = "newsite/swagger";
    });
}


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Registration}/{action=SignInForm}/{id?}");

app.Run();
