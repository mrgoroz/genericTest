using genericTest.Models;
using genericTest.Services;
using Microsoft.Extensions.DependencyInjection;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var services = RegisterServices().BuildServiceProvider();
        var chainResource = services.GetService(typeof(IChainResource<ResType>));
        var res = await (chainResource as IChainResource<ResType>).GetValue();
        Console.WriteLine(res.ToString());  
    }
    private static ServiceCollection RegisterServices()
    {
        var services = new ServiceCollection();
        services.AddSingleton<IDateTimeChecker, DateTimeChecker>();

        services.AddSingleton(typeof(IChainResource<>), typeof(ChainResource<>));
        services.AddSingleton(typeof(IWebServiceReader<>), typeof(WebServiceReader<>));
        services.AddSingleton(typeof(IFileSystemReader<>), typeof(FileSystemReader<>));
        services.AddSingleton(typeof(IMemoryReader<>), typeof(MemoryReader<>));

        services.AddHttpClient();

        return services;
    }
}


