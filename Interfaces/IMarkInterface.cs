using Project.Models;

namespace Project.Interfaces
{
    public interface IMarkInterface
    {
        Task<IEnumerable<Mark>> GetMarks();
        Task<Mark> GetMark(Guid markId);
        Task<bool> MarkExists(Guid markId);
        Task<bool> CreateMark(Mark mark);
        Task<bool> UpdateMark(Mark mark);
        Task<bool> DeleteMark(Mark mark);
        Task<bool> Save();
    }
}