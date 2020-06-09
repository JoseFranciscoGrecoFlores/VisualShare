using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;
using Blazor.FileReader;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VisualShare.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            
            builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ApiAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>(x => x.GetService<ApiAuthenticationStateProvider>());
            builder.Services.AddTransient(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            
            await builder.Build().RunAsync();
        }
    }
}
