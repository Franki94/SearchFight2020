using Microsoft.Extensions.DependencyInjection;
using SearchFight.Exceptions;
using SearchFight.Orchestrators;
using SearchFight.Validators;
using SearchFight.Views;
using System;
using System.Collections.Generic;

namespace SearchFight
{
    class Program
    {
        private static SearchFightConsoleView view;
        static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;

            ServiceProvider provider = ServiceCollectionHandler.GetServiceProvider();

            var validator = provider.GetRequiredService<SearchFightValidator>();
            view = provider.GetRequiredService<SearchFightConsoleView>();
            var orchestrator = provider.GetRequiredService<ISearchFightOrchestrator>();


            validator.Validate(args);

            var response = orchestrator.GetSearchFightViewTotalResponse(args);
            view.DisplaySearchFightResults(response);
        }
        static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
        {
            var exceptions = new Dictionary<Type, string>
            {
                [typeof(ValidatorException)] = "Exception in Validator:",
                [typeof(ApiClientExceptions)] = "Exception in Api Client:"
            };

            if (exceptions.ContainsKey(e.ExceptionObject.GetType()))
                view.DisplayErrorMessages($"{exceptions.GetValueOrDefault(e.ExceptionObject.GetType())} {e.ExceptionObject}");
            else
                view.DisplayErrorMessages(e.ExceptionObject.ToString());

            Environment.Exit(0);
        }
    }
}
