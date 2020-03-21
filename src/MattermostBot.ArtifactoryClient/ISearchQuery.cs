using System.Threading.Tasks;

namespace MattermostBot.ArtifactoryClient
{
    public interface ISearchQuery
    {
        string Body { get; }

        Task<ArtifactsList> Execute();
    }
}
