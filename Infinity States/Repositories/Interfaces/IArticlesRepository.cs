using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity_States.Data;

public interface IArticlesRepository
{
    public Task<Article> GetArticle(int id);
    public Task<List<Article>> GetAllArticles();
    public Task Create(Article article);
    public Task Update(int id, MutableArticleData data);
    public Task<List<Article>> GetArticlesWithFilter(int filter);
    public Task<List<Article>> GetArticlesByFollowedAuthors(int userId);
}