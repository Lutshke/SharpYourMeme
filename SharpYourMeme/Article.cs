namespace SharpYourMeme;

public class Article
{
    public string Title { get; }

    public Article(string title, string articleUrl, string imageUrl, string about)
    {
        Title = title;
        ArticleUrl = articleUrl;
        ImageUrl = imageUrl;
        About = about;
    }

    public string ArticleUrl { get; }
    public string ImageUrl { get; }
    public string About { get; }

    public static async Task<SearchResult> GetArticle(string query)
    {
        return await MemeSearch.GetArticle(query);
    }

    public static async Task<SearchResult> GetArticles(string query)
    {
        return await MemeSearch.GetArticles(query);
    }

    public static async Task<SearchResult> GetArticleFromUrl(string url)
    {
        return await MemeSearch.GetArticleFromUrl(url);
    }

}
