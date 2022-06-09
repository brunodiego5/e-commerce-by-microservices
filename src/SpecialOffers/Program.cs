var builder = WebApplication.CreateBuilder(args);

builder.Services.Scan(selector => 
    selector
        .FromAssemblyOf<Program>()
        .AddClasses(c =>
            c.Where(t =>
              //t != typeof(ProductCatalogClient) && 
              //t != typeof(SqlEventStore) && 
              t.GetMethods().All(m => m.Name != "<Clone>$")))
        .AsImplementedInterfaces());

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();