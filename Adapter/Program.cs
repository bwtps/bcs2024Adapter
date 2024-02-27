using Adapter;
using Adapter.InjectionModules;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Serilog;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureAppConfiguration(app =>
{

})
.ConfigureHostConfiguration(builder =>
{
    //Microsoft.Extensions.Configuration.Json
    //builder.SetBasePath(AppDomain.CurrentDomain.BaseDirectory);
    //builder.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
})
.ConfigureServices((hostContext, services) =>
 {
     //services.AddPooledDbContextFactory<DatabaseDbContext>(options =>
     //         options.UseNpgsql(hostContext.Configuration.GetConnectionString("database")), poolSize: 128);
 })
.ConfigureLogging((hostContext, logging) =>
{
    var logger = new LoggerConfiguration().ReadFrom.Configuration(hostContext.Configuration).CreateLogger();
    logging.AddSerilog(logger);
})
.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(containerbuilder =>
{
    containerbuilder.RegisterModule<AutofacPropertityModuleRegister>();
})
.ConfigureServices(services =>
{
    //services.AddHostedService<Worker>();
});

var host = builder.Build();
host.Run();
