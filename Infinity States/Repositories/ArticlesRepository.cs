using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity_States.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Infinity_States.Services;

public class ArticlesRepository : IArticlesRepository
{
    private readonly ApplicationContext db = new ApplicationContext();

    public async Task<Article> GetArticleAsync(int id) => await db.Articles.FindAsync(id);

    public async Task<List<Article>> GetAllArticlesAsync() => await db.Articles.ToListAsync();

    public async Task CreateArticle(Article article) 
    {
        db.Articles.Add(article);
        await db.SaveChangesAsync();
    }
}