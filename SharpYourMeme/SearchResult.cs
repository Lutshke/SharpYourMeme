namespace SharpYourMeme;

public class SearchResult
{
    public bool ArticlesFound { get; }

    public Article[] Articles { get; }

    public SearchResult(bool articlesFound, Article[] articles)
    {
        ArticlesFound = articlesFound;
        Articles = articles;
    }
}
