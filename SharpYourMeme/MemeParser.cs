using HtmlAgilityPack;

namespace SharpYourMeme;

public static class MemeSearch
{
    private static HttpClient HttpClient { get; }
    private const string GridXPath = "/html/body/div[6]/div/div[3]/div[2]/div/table/tbody";
    private const string AboutXPath = "/html/body/div[5]/div/article/div[4]/section/div[1]/div[1]/div[1]/div/p";

    static MemeSearch()
    {
        HttpClient = new HttpClient();
        HttpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:99.0) Gecko/20100101 Firefox/99.0");
    }

    private static string GetQueryUrl(string query)
    {
        return "https://knowyourmeme.com/search?q=" + query;
    }

    private static string GetArticleUrl(string query)
    {
        return "https://knowyourmeme.com" + query;
    }

    private static async Task<List<Article>> GetArticlesFromHtml(HtmlDocument htmlDoc, bool getSingleArticle)
    {
        var docNode = htmlDoc.DocumentNode;
        var collectionNode = docNode.SelectSingleNode(GridXPath);

        var articles = new List<Article>();

        if (getSingleArticle)
        {
            var firstArticle = collectionNode.ChildNodes
                .Where(node => node.NodeType == HtmlNodeType.Element)
                .First().ChildNodes
                .Where(node => node.NodeType == HtmlNodeType.Element)
                .First();
            var art = await GetArticleInfo(firstArticle);
            articles.Add(art);
        }
        else
        {
            for (int row = 0; row < collectionNode.ChildNodes.Count; row++)
            {
                var rows = collectionNode.ChildNodes[row];

                if (rows.NodeType == HtmlNodeType.Element)
                {
                    for (int article = 0; article < rows.ChildNodes.Count; article++)
                    {
                        var artNode = rows.ChildNodes[article];
                        if (artNode.NodeType == HtmlNodeType.Element)
                        {
                            try
                            {
                                var art = await GetArticleInfo(artNode);
                                if (art is not null)
                                {
                                    articles.Add(art);
                                }
                            }
                            catch (Exception)
                            {
                                continue;
                            }

                        }
                    }
                }
            }
        }

        return articles;
    }

    private static async Task<Article> GetArticleInfo(HtmlNode articleNode)
    {
        var imageNode = articleNode.ChildNodes
                .Where(node => node.NodeType == HtmlNodeType.Element)
                .First().ChildNodes
                .Where(node => node.NodeType == HtmlNodeType.Element)
                .First();
        var title = imageNode.GetAttributeValue("title", "no title found");
        var articleUrl = articleNode.ChildNodes.Where(node => node.NodeType == HtmlNodeType.Element)
                .First().GetAttributeValue("href", "url not available");
        articleUrl = GetArticleUrl(articleUrl);
        var imageUrl = imageNode.GetAttributeValue("src", "no image found");

        var html = await HttpClient.GetStringAsync(articleUrl);

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        var aboutNode = htmlDoc.DocumentNode.SelectSingleNode(AboutXPath);

        if (aboutNode is null) return null;

        var about = aboutNode.InnerText;

        return new Article(title, articleUrl, imageUrl, about);
    }

    /// <summary>
    /// This searches and returns a single article
    /// </summary>
    /// <param name="query"></param>
    /// <param name="articles"></param>
    /// <returns></returns>
    public static async Task<SearchResult> GetArticle(string query)
    {
        var url = GetQueryUrl(query);

        var html = await HttpClient.GetStringAsync(url);

        if (html.Contains("Sorry, but there were no results for"))
        {
            return new SearchResult(false, null);
        }

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        var articles = await GetArticlesFromHtml(htmlDoc, true);

        return new SearchResult(true, articles);
    }

    /// <summary>
    /// You can use this to get a collection of articles but its slow af (not recomended)
    /// </summary>
    /// <param name="query"></param>
    /// <param name="articles"></param>
    /// <returns></returns>
    public static async Task<SearchResult> GetArticles(string query)
    {
        var url = GetQueryUrl(query);

        var html = await HttpClient.GetStringAsync(url);

        if (html.Contains("Sorry, but there were no results for"))
        {
            return new SearchResult(false, null);
        }

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        var articles = await GetArticlesFromHtml(htmlDoc, false);

        return new SearchResult(true, articles);
    }

    /// <summary>
    /// Use this to get a specifc article by url
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public static async Task<SearchResult> GetArticleFromUrl(string url)
    {
        var html = await HttpClient.GetStringAsync(url);

        if (html.Contains("Sorry, but there were no results for"))
        {
            return new SearchResult(false, null);
        }

        var htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(html);
        var articles = await GetArticlesFromHtml(htmlDoc, true);

        return new SearchResult(true, articles);
    }
}
