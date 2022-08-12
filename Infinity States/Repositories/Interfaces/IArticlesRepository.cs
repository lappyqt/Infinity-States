using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity_States.Data;
using Microsoft.AspNetCore.Hosting;

public interface IArticlesRepository
{
    public Task<Article> GetArticle(int id);
    public Task<IEnumerable<Article>> GetAllArticles();
    public Task Create(Article article);
    public Task Update(int id, MutableArticleData data);
    public Task Delete(int id, string author);
    public Task<IEnumerable<Article>> GetArticlesWithFilter(int filter);
    public Task<IEnumerable<Article>> GetArticlesByFollowedAuthors(int userId);
}