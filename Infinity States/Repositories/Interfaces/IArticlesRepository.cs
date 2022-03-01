using System.Collections.Generic;
using System.Threading.Tasks;
using Infinity_States.Data;

public interface IArticlesRepository
{
    Task<Article> GetArticleAsync(int id);
    Task<List<Article>> GetAllArticlesAsync();
    Task CreateArticle(Article article);
}