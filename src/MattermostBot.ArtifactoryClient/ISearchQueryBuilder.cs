using System;

namespace MattermostBot.ArtifactoryClient
{
    public interface ISearchQueryBuilder
    {
        IWhereRepository WhereRepository();

        IWhereName WhereName();

        ISearchQueryBuilder Or(params Action<ISearchQueryBuilder>[] builds);

        ISearchQuery Build();
    }
}
