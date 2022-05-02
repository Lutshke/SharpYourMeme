namespace SharpYourMeme;

public class SearchResult
{
    public bool ArticlesFound { get; }

    public List<Article>? Articles { get; }

    public SearchResult(bool articlesFound, List<Article>? articles)
    {
        ArticlesFound = articlesFound;
        Articles = articles;
    }
}
