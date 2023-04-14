using MyBlogSiteCore.Models;

namespace MyBlogSiteCore.Services.Interfaces
{
    public interface IPostings
    {
        Task<List<Posting>> GetAllAsync();
        Task<Posting> GetAsync(int? id);
        Task<Posting> AddAsync(Posting model);
        Task<Posting> EditAsync(Posting model);
        void Delete(Posting model);
    }
}
