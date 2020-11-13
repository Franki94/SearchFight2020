using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;

namespace SearchFight.Searcher.Extensions
{
    public static class SearcherExtensions
    {
        public static string ToQueryString(this Dictionary<string, string> values)
        {
            var queryString = new StringBuilder("?");
            foreach (var item in values)
                queryString.Append($"{item.Key}={item.Value}&");

            if (values.Count > 0)
                queryString.Remove(queryString.Length - 1, 1);

            return queryString.ToString();
        }

        public static T ToObjectResponse<T>(this string values)
        {
            return JsonConvert.DeserializeObject<T>(values);
        }
    }
}
