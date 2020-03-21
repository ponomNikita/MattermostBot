using System;
using System.Threading.Tasks;

namespace MattermostBot.ArtifactoryClient
{
    public class SearchQuery : ISearchQuery
    {
        private IArtifactory _artifactory;
        
        public SearchQuery(IArtifactory artifactory, string body)
        {
            this.Body = body;
            _artifactory = artifactory ?? throw new ArgumentNullException(nameof(artifactory));
        }

        public string Body { get; }

        public Task<ArtifactsList> Execute()
        {
            return _artifactory.Execute(this);
        }
    }
}
