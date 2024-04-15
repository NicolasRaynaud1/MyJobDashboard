using DashboardJob.Services;
using DashboardJob.Services.Interfaces;
using MyJobDashboard.Components;
using MyJobDashboard.Models;
using MyJobDashboard.Services;
using MyJobDashboard.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Cache
builder.Services.AddMemoryCache();

//Services
builder.Services.AddScoped<IAccessTokenService, AccessTokenService>();
builder.Services.AddScoped<ICacheService, CacheService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

// Récupération de l'accessToken au lancement de l'appli et stockage dans le cache
using (var serviceScope = app.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;
    var accessTokenService = services.GetRequiredService<IAccessTokenService>();
    var cacheService = services.GetRequiredService<ICacheService>();

    AccessToken? token = await accessTokenService.GetAccessTokenAsync();

    if (token is not null)
        cacheService.SetToken(token.TokenString, TimeSpan.FromSeconds(token.Expiration - 300));

    //var test = cacheService.GetToken();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
