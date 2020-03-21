namespace MattermostBot.ArtifactoryClient
{
    public class WhereRepository : IWhereRepository
    {
        private readonly SearchQueryBuilder _query;

        public WhereRepository(SearchQueryBuilder query)
        {
            _query = query;
        }

        public ISearchQueryBuilder Equal(string value)
        {
            _query.AddClause($"\"repo\": {{ \"$eq\": \"{value}\" }}");

            return _query;
        }

        public ISearchQueryBuilder Match(string value)
        {
            _query.AddClause($"\"repo\": {{ \"$match\": \"{value}\" }}");

            return _query;
        }

        public ISearchQueryBuilder NotEqual(string value)
        {
            _query.AddClause($"\"repo\": {{ \"$ne\": \"{value}\" }}");

            return _query;
        }
    }
}
