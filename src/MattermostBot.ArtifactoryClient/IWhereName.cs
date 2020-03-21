namespace MattermostBot.ArtifactoryClient
{
    public interface IWhereName
    {
        ISearchQueryBuilder Match(string value);


        ISearchQueryBuilder Equal(string value);
        
        ISearchQueryBuilder NotEqual(string value);

    }
}
