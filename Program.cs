using MeuPontoOnline.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// chama a conexão com o supabase
var config = builder.Configuration.GetSection("Supabase");
var url = config["Url"];
var key = config["Key"];

if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(key))
{
    throw new ArgumentNullException("Supabase URL e Key não podem ser nulos.");
}

builder.Services.AddScoped<GeoLocalizacaoService>();

var options = new Supabase.SupabaseOptions
{
    AutoRefreshToken = true,
    AutoConnectRealtime = true
};

builder.Services.AddSingleton(provider =>
{
    var supabase = new Supabase.Client(url, key, options);
    supabase.InitializeAsync().Wait(); 
    return supabase;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Login";
        options.AccessDeniedPath = "/AcessoNegado"; // se quiser tratar acesso negado
    });

builder.Services.AddAuthorization();

builder.Services.AddRazorPages();

var app = builder.Build();

// Pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication(); // <- ESSENCIAL PARA LOGIN FUNCIONAR
app.UseAuthorization();

app.MapRazorPages();
app.Run();
