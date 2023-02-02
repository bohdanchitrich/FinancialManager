using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Events;
using UI.Mapper;
using UI.Services;
using UI.Services.Category;
using UI.Services.FinancialOperation;
using UI.Services.Report;
using UI.Services.Shared.TokenManager;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((_, lx) =>
{
    lx.WriteTo.Console(LogEventLevel.Debug);
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddHttpClient("ApiClient", option =>
{
    option.BaseAddress = new Uri(builder.Configuration["ApiBaseAddress"]);
});

builder.Services.AddScoped<IFinancialManagerHttpClient, FinancialManagerHttpClient>();
builder.Services.AddSingleton<IConfigurationRoot>(builder.Configuration);
builder.Services.AddScoped<IJwtTokenManager, JwtTokenManager>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IFinancialOperationService, FinancialOperationService>();
builder.Services.AddScoped<IReportService, ReportService>();
//Jwt
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie("Cookies").AddOpenIdConnect("oidc", options =>
{

    options.Authority = builder.Configuration["Jwt:Authority"];
    options.ClientId = builder.Configuration["Jwt:ClientId"];
    options.MetadataAddress = $"{builder.Configuration["Jwt:Authority"]}/.well-known/openid-configuration";
    options.RequireHttpsMetadata = false;
    options.ClientSecret = builder.Configuration["Jwt:ClientSecret"];
    options.ResponseType = builder.Configuration["Jwt:ResponseType"];
    options.SaveTokens = true;
    options.GetClaimsFromUserInfoEndpoint = true;
    options.TokenValidationParameters = new TokenValidationParameters { NameClaimType = "name" };

    options.Events = new OpenIdConnectEvents
    {
        OnAccessDenied = context =>
        {
            context.HandleResponse();
            context.Response.Redirect("/");
            return Task.CompletedTask;
        }
    };
});
//Mapper
builder.Services.AddAutoMapper(typeof(AppMappingProfile));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}


app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.UseRouting();


app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
