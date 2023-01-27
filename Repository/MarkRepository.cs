using Project.Data;
using Project.Interfaces;
using Project.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Project.Repository
{
    public class MarkRepository : IMarkInterface
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MarkRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateMark(Mark mark)
        {
            await _context.AddAsync(mark);
            return await Save();
        }

        public async Task<bool> DeleteMark(Mark mark)
        {
            _context.Remove(mark);
            return await Save();
        }

        public async Task<Mark> GetMark(Guid markId)
        {
            return await _context.Marks
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == markId);
        }

        public async Task<IEnumerable<Mark>> GetMarks()
        {
            return await _context.Marks
                .AsNoTracking()
                .OrderBy(m => m.Name)
                .ToListAsync();
        }

        public async Task<bool> MarkExists(Guid markId)
        {
            return await _context.Marks.AnyAsync(m => m.Id == markId);
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateMark(Mark mark)
        {
            _context.Update(mark);
            return await Save();
        }
    }
}