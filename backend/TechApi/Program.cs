var builder = WebApplication.CreateBuilder(args);
var MyAllowSecificOrigins = "_MyAllowSecificOrigins";
// Add services to the container.
builder.Services.AddControllersWithViews();

// ⭐ ADD THIS (Important)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSecificOrigins,
        builder =>
        {
            builder.WithOrigins("http://localhost", "http://localhost:4200").AllowAnyHeader().AllowAnyMethod().SetIsOriginAllowedToAllowWildcardSubdomains();
        });
});

var app = builder.Build();

// ⭐ REMOVE if condition and use this directly
app.UseSwagger();

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "TechApi v1");
    c.RoutePrefix = string.Empty;   // Makes Swagger default page
});

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors(MyAllowSecificOrigins);
app.UseAuthorization();

// ❌ REMOVE THIS (if you don't need MVC Home page)
// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllers();

app.Run();