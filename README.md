# SharpYourMeme
A C# Interface for the KnowYourMeme Website
Written in NET 6.0 and is extremly slow

I tried to make it as user friendly as possible

**To get a single Article**
```cs
using SharpYourMeme;

SearchResult result = await MemeSearch.GetArticle("pepe");
```


**To get a multiple Articles (I do NOT recommend this option because its very slow)**
```cs
using SharpYourMeme;

SearchResult result = await MemeSearch.GetArticles("pepe");
```

**Or get an article with url**
```cs
using SharpYourMeme;

SearchResult result = await MemeSearch.GetArticles("https://knowyourmeme.com/memes/shadilay");
```
