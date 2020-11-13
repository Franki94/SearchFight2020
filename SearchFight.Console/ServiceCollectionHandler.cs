using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SearchFight.Orchestrators;
using SearchFight.Searcher;
using SearchFight.Searcher.Bing;
using SearchFight.Searcher.Google;
using SearchFight.Service;
using SearchFight.Validators;
using SearchFight.Views;
using System;

namespace SearchFight
{
    class ServiceCollectionHandler
    {
        public static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();

            var googleConfig = configuration.GetSection("google").Get<GoogleSearcherSecret>();
            serviceCollection.AddSingleton(googleConfig);
            var bingConfig = configuration.GetSection("bing").Get<BingSearcherSecret>();

            serviceCollection.AddHttpClient<ISearcher, BingSearcher>("bing", client =>
            {             
                client.BaseAddress = new Uri(bingConfig.Url);
                client.DefaultRequestHeaders.Add(bingConfig.DefaultHeaders[0].Name, bingConfig.DefaultHeaders[0].Value);
            });
            serviceCollection.AddHttpClient<ISearcher, GoogleSearcher>("google", client =>
            {                
                client.BaseAddress = new Uri(googleConfig.Url);
            });

            serviceCollection.AddTransient<ISearchFightService, SearchFightService>();
            serviceCollection.AddTransient<ISearcherCompetition, SearcherCompetition>();
            serviceCollection.AddTransient<ISearchFightOrchestrator, SearchFightOrchestrator>();
            serviceCollection.AddSingleton<SearchFightValidator>();
            serviceCollection.AddSingleton<SearchFightConsoleView>();

            return serviceCollection.BuildServiceProvider();
        }
    }
}
