using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpYourMeme;

public class Article
{
    public string Title { get; }

    public Article(string title, string articleUrl, string imageUrl,string about)
    {
        Title = title;
        ArticleUrl = articleUrl;
        ImageUrl = imageUrl;
        About = about;
    }

    public string ArticleUrl { get; }
    public string ImageUrl { get; }
    public string About { get; }

}
