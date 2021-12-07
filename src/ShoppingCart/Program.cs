using Polly;
using ShoppingCart.Domain.Interfaces;
using ShoppingCart.Infrastructure.Services;

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

builder.Services.AddHttpClient<IProductCatalogClient, ProductCatalogClient>()
    .AddTransientHttpErrorPolicy(p =>
        p.WaitAndRetryAsync(3, attempt => 
            TimeSpan.FromMilliseconds(100*Math.Pow(2, attempt))));

var app = builder.Build();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
