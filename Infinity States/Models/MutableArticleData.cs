using Microsoft.AspNetCore.Http;

namespace Infinity_States.Data;

public class MutableArticleData
{
    public IFormFile Poster { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public MutableArticleData(IFormFile poster, string title, string content)
    {
        Poster = poster;
        Title = title;
        Content = content;
    }
}