using Microsoft.EntityFrameworkCore;
using MyBlogSiteCore.Data;
using MyBlogSiteCore.Models;
using MyBlogSiteCore.Services.Interfaces;

namespace MyBlogSiteCore.Services
{
    public class EFPostingsService : IPostings
    {
        private readonly ApplicationDbContext _context;

        public EFPostingsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Posting> AddAsync(Posting model)
        {
            _context.Add(model);
            await _context.SaveChangesAsync();
         
            return model;
        }

        public void Delete(Posting model)
        {
            _context.Postings.Remove(model);
            _context.SaveChanges();
        }

        public async Task<Posting> EditAsync(Posting model)
        {
            _context.Update(model);
            await _context.SaveChangesAsync();

            return model;
        }

        public async Task<List<Posting>> GetAllAsync()
        {
            return await _context.Postings.ToListAsync();
        }

        public async Task<Posting> GetAsync(int? id)
        {
            if (id == null || _context.Postings == null)
            {
                throw new NotImplementedException();
            }

            //var posting = await _context.Postings.FindAsync(id);
            var posting = await _context.Postings
                .FirstOrDefaultAsync(m => m.Id == id);

            return posting ?? throw new NotImplementedException();
        }
    }
}
