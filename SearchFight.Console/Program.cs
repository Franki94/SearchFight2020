using Microsoft.Extensions.DependencyInjection;
using SearchFight.Exceptions;
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
    class Program
    {
        private static SearchFightConsoleView view;
        static void Main(string[] args)
        {
            System.AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            ServiceProvider provider = GetServiceProvider();

            var validator = provider.GetRequiredService<SearchFightValidator>();
            view = provider.GetRequiredService<SearchFightConsoleView>();
            var orchestrator = provider.GetRequiredService<ISearchFightOrchestrator>();
            
            validator.Validate(args);

            var response = orchestrator.GetSearchFightViewTotalResponse(args);
            view.DisplaySearchFightResults(response);
        }
        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject.GetType() == typeof(ValidatorException))
                view.DisplayErrorMessages($"Exception in Validator: {e.ExceptionObject}");
            else if (e.ExceptionObject.GetType() == typeof(ApiClientExceptions))
                view.DisplayErrorMessages($"Exception in Api Client: {e.ExceptionObject}");
            else
                view.DisplayErrorMessages(e.ExceptionObject.ToString());
            Environment.Exit(0);
        }
        private static ServiceProvider GetServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(new GoogleSearcherSecret { Key = "key", Cx = "cx" });

            serviceCollection.AddHttpClient<ISearcher, BingSearcher>("bing", client =>
            {
                client.BaseAddress = new Uri("https://api.bing.microsoft.com/");
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "key");
            });
            serviceCollection.AddHttpClient<ISearcher, GoogleSearcher>("google", client =>
            {
                client.BaseAddress = new Uri("https://www.googleapis.com");
            });
            serviceCollection.AddTransient<ISearchFightService, SearchFightService>();
            serviceCollection.AddTransient<ISearcherCompetition, SearcherCompetition>();
            serviceCollection.AddTransient<ISearchFightOrchestrator, SearchFightOrchestrator>();
            serviceCollection.AddSingleton<SearchFightValidator>();
            serviceCollection.AddSingleton<SearchFightConsoleView>();


            var provider = serviceCollection.BuildServiceProvider();
            return provider;
        }
    }
}
