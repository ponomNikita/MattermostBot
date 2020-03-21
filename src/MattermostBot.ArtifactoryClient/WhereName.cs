namespace MattermostBot.ArtifactoryClient
{
    internal class WhereName : IWhereName
    {
        private readonly SearchQueryBuilder _query;

        public WhereName(SearchQueryBuilder query)
        {
            _query = query;
        }

        public ISearchQueryBuilder Equal(string value)
        {
            _query.AddClause($"\"name\": {{ \"$eq\": \"{value}\" }}");

            return _query;
        }

        public ISearchQueryBuilder Match(string value)
        {
            _query.AddClause($"\"name\": {{ \"$match\": \"{value}\" }}");

            return _query;
        }

        public ISearchQueryBuilder NotEqual(string value)
        {
            _query.AddClause($"\"name\": {{ \"$ne\": \"{value}\" }}");

            return _query;
        }
    }
}
