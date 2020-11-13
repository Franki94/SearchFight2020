using System.Collections.Generic;

namespace SearchFight.Searcher.Google
{
    public class GoogleSearcherSecret
    {
        public GoogleSearcherSecret()
        {
            Params = new List<GoogleSearcherSecretParams>();
        }
        public string Url { get; set; }
        public List<GoogleSearcherSecretParams> Params { get; set; }
    }

    public class GoogleSearcherSecretParams 
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
