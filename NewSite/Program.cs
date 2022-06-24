using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Repository;
using Repository.Service.Impl;
using System.Text;
using Repository.Service.Impl.Profile;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Repository.Interface;
using Repository.Impl;
using Service.Interface;
using Service.Impl;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Helper_s;
using Helper_s.Implementations;
using SignalR.Server;
using SignalR.Server.Impl;
using SignalR.Server.Interface;
using SignalRClient.Client.Interface;
using SignalRClient.Client.Impl;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSession();
builder.Services.AddControllersWithViews()
                .AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddSignalR();

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
builder.Services.AddScoped<IAbstractCaching, AbstractCaching>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IUserDetailsRepository, UserDetailsRepository>();
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<IFriendRequestService, FriendRequestService>();
builder.Services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
builder.Services.AddScoped<IHandler, Handler>();
builder.Services.AddScoped<IClient, Client>();
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

app.UseResponseCaching();
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
app.MapHub<ChatHub>("/chatHub");
app.Run();
