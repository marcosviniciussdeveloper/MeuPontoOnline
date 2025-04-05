var builder = WebApplication.CreateBuilder(args);

// Carrega configurações do appsettings.json
var config = builder.Configuration.GetSection("Supabase");
var url = config["Url"];
var key = config["Key"];

var options = new Supabase.SupabaseOptions
{
    AutoConnectRealtime = true
};

// Registra o cliente Supabase como Singleton no container DI
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
