# SharpYourMeme
A C# Interface for the KnowYourMeme Website
Written in NET 6.0.

I tried to make it as user friendly as possible

**To get a single Article**
```cs
using SharpYourMeme;

SearchResult result = await Article.GetArticle("pepe");
```


**To get a multiple Articles**
```cs
using SharpYourMeme;

SearchResult result = await Article.GetArticles("pepe");
```

**Or get an article with url**
```cs
using SharpYourMeme;

SearchResult result = await Article.GetArticles("https://knowyourmeme.com/memes/shadilay");
```
