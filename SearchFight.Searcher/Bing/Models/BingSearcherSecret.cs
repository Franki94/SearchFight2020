using System.Collections.Generic;

namespace SearchFight.Searcher.Bing
{
    public class BingSearcherSecret
    {
        public BingSearcherSecret()
        {
            DefaultHeaders = new List<BingSearcherSecretParams>();
        }
        public string Url { get; set; }
        public List<BingSearcherSecretParams> DefaultHeaders { get; set; }
    }
    public class BingSearcherSecretParams
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
