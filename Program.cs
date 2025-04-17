using MeuPontoOnline.Services;

var builder = WebApplication.CreateBuilder(args);

//chama a conexão com o supabase
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
    supabase.InitializeAsync().Wait(); // Use Wait() pois não pode ser async aqui
    return supabase;
});




// Adiciona Razor Pages
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
app.UseAuthorization();

app.MapRazorPages();
app.Run();
