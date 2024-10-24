using AppAspnetMvc.Models;

var builder = WebApplication.CreateBuilder(args);

// passando os valores de conexão e de database para o contexto
ContextMongoDb.ConnectionString = builder.Configuration.GetSection("MongoDbConnection:ConnectionString").Value;
ContextMongoDb.DatabaseName = builder.Configuration.GetSection("MongoDbConnection:DatabaseName").Value;

// adicionando um singleton para nosso contexto
builder.Services.AddSingleton<ContextMongoDb>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
