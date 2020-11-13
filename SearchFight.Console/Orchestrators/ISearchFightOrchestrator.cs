namespace SearchFight.Orchestrators
{
    public interface ISearchFightOrchestrator
    {
        SearchFightViewTotalResponse GetSearchFightViewTotalResponse(string[] technologies);
    }
}
