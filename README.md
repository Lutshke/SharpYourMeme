# SharpYourMeme
A C# Interface for the KnowYourMeme Website
Written in NET 6.0 and is extremly slow

I tried to make it as user friendly as possible

**To get a single Article**
```cs
using SharpYourMeme;

SearchResult result = await MemeSearch.GetArticle("pepe");
```


**To get a multiple Articles**
```cs
using SharpYourMeme;

SearchResult result = await MemeSearch.GetArticles("pepe");
```
