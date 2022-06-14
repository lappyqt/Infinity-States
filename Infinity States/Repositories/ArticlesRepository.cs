using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity_States.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infinity_States.Repositories;

public class ArticlesRepository : IArticlesRepository
{
    private readonly ApplicationContext db = new ApplicationContext();

    public async Task<Article> GetArticle(int id) 
    {
        return await db.Articles.FindAsync(id);
    } 

    public async Task<IEnumerable<Article>> GetAllArticles()
    {
        return await db.Articles.ToListAsync();
    }

    public async Task Create(Article article) 
    {
        db.Articles.Add(article);
        await db.SaveChangesAsync();
    }

    public async Task Update(int id, MutableArticleData data)
    {
        Article article = await db.Articles.FirstOrDefaultAsync(x => x.Id == id);

        string pathToPoster = $"/files/images/{data.Poster.FileName}" ?? article.Poster;

        article.Poster = pathToPoster;
        article.Title = data.Title;
        article.Content = data.Content;
 
        await db.SaveChangesAsync();
    }

    public async Task Delete(int id, string author)
    {
        Article article = db.Articles.FirstOrDefault(x => x.Id == id);

        if (article != null && article.Author == author)
        {
            db.Articles.Remove(article);
            await db.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Article>> GetArticlesWithFilter(int filter)
    {
        return await db.Articles.Where(x => x.Category == filter).ToListAsync();
    }

    public async Task<IEnumerable<Article>> GetArticlesByFollowedAuthors(int userId)
    {
        User user = await db.Users.FindAsync(userId);
        List<Article> result = new List<Article>();

        foreach(string author in user.Authors)
        {
            Article article = await db.Articles.Where(data => data.Author == author).OrderBy(data => data.Id).LastOrDefaultAsync();
            if (article is not null) result.Add(article);            
        }

        return result;
    }
}