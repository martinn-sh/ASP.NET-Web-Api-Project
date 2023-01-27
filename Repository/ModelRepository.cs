using Project.Data;
using Project.Interfaces;
using Project.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository
{
    public class ModelRepository : IModelInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ModelRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> CreateModel(Model model)
        {
            await _context.AddAsync(model);
            return await Save();
        }

        public async Task<bool> DeleteModel(Model model)
        {
            _context.Remove(model);
            return await Save();
        }

        public async Task<Model> GetModel(Guid modelId)
        {
            return await _context.Models
                .AsNoTracking()
                .Include(m => m.Mark)
                .FirstOrDefaultAsync(m => m.Id == modelId);
        }

        public async Task<IEnumerable<Model>> GetModels()
        {
            return await _context.Models
                .AsNoTracking()
                .Include(m => m.Mark)
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<bool> ModelExists(Guid modelId)
        {
            return await _context.Models.AnyAsync(m => m.Id == modelId);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateModel(Model model)
        {
            _context.Update(model);
            return await Save();
        }
    }
}