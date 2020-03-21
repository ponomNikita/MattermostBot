using System.Threading.Tasks;

namespace MattermostBot.ArtifactoryClient
{
    public interface IArtifactory
    {
        ISearchQueryBuilder Search();

        Task<ArtifactsList> Execute(ISearchQuery query);
    }
    
}
