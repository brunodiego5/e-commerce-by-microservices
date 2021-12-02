using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
var app = builder.Build();


app.UseHttpsRedirection();
//app.UseRouting();
//app.UseEndpoints(endpoints =>
//endpoints.MapControllers());
app.MapControllers();
app.MapGet("/", () => "Hello World!");

app.Run();
