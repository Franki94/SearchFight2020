using SearchFight.Orchestrators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchFight.Views
{
    public class SearchFightConsoleView
    {
        public void DisplaySearchFightResults(SearchFightViewTotalResponse searchFightTotalResponse)
        {
            var technologies = string.Join(", ", searchFightTotalResponse.SearchFightResults.Select(x => x.Technology).Distinct());

            Console.WriteLine($"Search Fight between {technologies} Results");
            var averageResults = searchFightTotalResponse.SearchFightResults.GroupBy(x => x.Technology);
            foreach (var item in averageResults)
                Console.WriteLine(ResultGroupViewToString(item.Key, item.ToList()));

            Console.WriteLine();
            Console.WriteLine($"Search Fight between {technologies} Results Winners per searcher");
            foreach (var item in searchFightTotalResponse.SearchFightSearcherWinnersResults)
                Console.WriteLine($"{item.SearcherName} winner: {item.Technology}");

            Console.WriteLine();
            Console.WriteLine($"Search Fight between {technologies} Winner");
            Console.WriteLine($"Total Winner {searchFightTotalResponse.SearchFightTechnologyWinner}");
        }

        public void DisplayErrorMessages(string message)
        {
            Console.WriteLine("ERROR FOUND");
            Console.WriteLine(message);
            Console.WriteLine("Application FINISHED");
        }
        private string ResultGroupViewToString(string technology, List<SearchFightViewResponse> searchFightViewResponses)
        {
            var response = new StringBuilder($"{technology}: ");
            for (int i = 0; i < searchFightViewResponses.Count(); i++)
                response.Append($"{searchFightViewResponses[i].SearcherName}: {searchFightViewResponses[i].TotalResultsCount} ");

            return response.ToString();
        }
    }


}
