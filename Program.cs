using ProtelAppT.Components;
using MudBlazor.Services;
using Microsoft.EntityFrameworkCore;
using ProtelAppT.Data;
using ProtelAppT.Service;
using Microsoft.AspNetCore.Http;
using ProtelAppT.Repositories;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddMudServices();
builder.Services.AddDbContext<ProtelDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGenericRepository, GenericRepository>();
builder.Services.AddScoped<UsuarioService>();
QuestPDF.Settings.License = LicenseType.Community;
builder.Services.AddScoped<ReporteService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<AuthenticationStateService>();
builder.Services.AddDistributedMemoryCache();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
//app.UseSession();  no se pudo usar, se debio crear un middleware para el uso de session
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
