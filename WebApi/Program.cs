
const string ApiName = "ToDoList";


WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
try
{
    builder.Services
        .AddControllers()
        .ConfigureJsonApi();
    builder.Services.AddHealthChecks();

    builder.Services
        .RegisterServices(builder.Configuration);

    builder.Host.ConfigureContainer<ContainerBuilder>(b =>
    {
        b.RegisterType<WebHttpRequestContext>().As<IRequestContext>().InstancePerLifetimeScope();
    });

    var app = builder.Build();

    if (!app.Environment.IsProduction())
    {
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseRouting();
    app.UseAuthorization();
    app.MapControllers();

    app.MapHealthChecks("/diagnostics/ping");

    app.Run();

}
catch (Exception ex)
{
    throw;
}


/// <summary>
///     Program class. Public to be able to use WebApplicationFactory in integration tests
/// </summary>
public partial class Program { }