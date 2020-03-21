using System;
using System.Collections.Generic;
using System.Text;

namespace MattermostBot.ArtifactoryClient
{
    public class SearchQueryBuilder : ISearchQueryBuilder
    {
        private List<string> _clauses = new List<string>();
        private IArtifactory _artifactory;

        public SearchQueryBuilder()
        {
        }

        public SearchQueryBuilder(IArtifactory artifactory)
        {
            _artifactory = artifactory;
        }


        public IWhereName WhereName()
        {
            return new WhereName(this);
        }

        public ISearchQueryBuilder Or(params Action<ISearchQueryBuilder>[] builds)
        {
            var orQueryBuilder = new SearchQueryBuilder();

            foreach (var build in builds)
            {
                build(orQueryBuilder);
            }

            AddClause($"\"$or\": [ {orQueryBuilder.GetFindBody()} ]");

            return this;
        }

        public IWhereRepository WhereRepository()
        {
            return new WhereRepository(this);
        }

        public void AddClause(string clause)
        {
            _clauses.Add(clause);
        }

        public ISearchQuery Build()
        {
            if (_artifactory == null)
            {
                throw new Exception("You can't call build for inner query");
            }

            return new SearchQuery(_artifactory, $"items.find({GetFindBody()})");
        }

        private string GetFindBody()
        {
            var sb = new StringBuilder();

            for (int i = 0; i < _clauses.Count; i++)
            {
                sb.Append($"{{ {_clauses[i]} }}");

                if (i != _clauses.Count - 1)
                {
                    sb.Append(", ");
                }                
            }

            return sb.ToString();
        }
    }
}
