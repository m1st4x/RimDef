using RimDef;
using System;
using System.Collections.Generic;

namespace SimpleSearch
{
    public class SearchCore
    {
        private List<Searcher> Searchers { get; set; }

        public SearchCore(List<Searcher> searchers)
        {
            //Add passed in searchers to the list of searchers to use.
            Searchers = searchers;
        }

        public IEnumerable<SearchResult> Search(string searchTerm)
        {
            var result = new List<SearchResult>();

            //Iterate over the collection of Searchers, calling their search method
            //and adding the result to the results to be returned.
            foreach (Searcher searcher in Searchers)
            {
                result.AddRange(searcher.Search(searchTerm));
            }
            return result;
        }

    }

    public abstract class Searcher
    {
        public abstract IEnumerable<SearchResult> Search(string searchTerm);
    }

    public class SearchResponse
    {
        public IEnumerable<SearchResult> Results { get; set; }
        public string OriginalSearchTerm { get; set; }
        public TimeSpan TimeTaken { get; set; }
    }

    public class SearchResult
    {
        //public string Label { get; set; }
        //public string Mod { get; set; }
        //public string Description { get; set; }
        public Def Definition { get; set; }
    }
}
