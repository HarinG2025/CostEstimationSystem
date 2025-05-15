var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<DataService>();


// ✅ Register controller services
builder.Services.AddControllers();

var app = builder.Build();

// Configure middleware here
app.UseStaticFiles();  // <-- THIS enables serving files from wwwroot

app.MapControllers();

app.Run();