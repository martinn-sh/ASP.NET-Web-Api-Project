using Project.Models;

namespace Project.Interfaces
{
    public interface IModelInterface
    {
        Task<IEnumerable<Model>> GetModels();
        Task<Model> GetModel(Guid modelId);
        Task<bool> ModelExists(Guid modelId);
        Task<bool> CreateModel(Model model);
        Task<bool> UpdateModel(Model model);
        Task<bool> DeleteModel(Model model);
        Task<bool> Save();
    }
}