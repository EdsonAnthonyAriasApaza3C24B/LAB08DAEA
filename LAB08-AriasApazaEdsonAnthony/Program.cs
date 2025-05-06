using LAB08_AriasApazaEdsonAnthony.Models;
using LAB08_AriasApazaEdsonAnthony.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Add DbContext (usa tu cadena de conexión real aquí)
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                       ?? "server=localhost;port=3306;user=root;password=ALTEC553210;database=LINQExample;";
builder.Services.AddDbContext<LinqexampleContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// 2. Registrar repositorios específicos y UnitOfWork
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IOrderDetailRepository, OrderDetailRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// 3. Agregar controladores
builder.Services.AddControllersWithViews();

// 4. Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "LINQExample API", Version = "v1" });
});

var app = builder.Build();

// Middleware pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "LINQExample API V1");
    });
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();