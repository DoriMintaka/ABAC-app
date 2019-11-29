using ABAC.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ABAC.DAL.Services.Contracts
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAsync();

        Task<T> GetAsync(int id);

        Task UpdateAsync(T model, int id);

        Task DeleteAsync(int id);

        Task<IEnumerable<Attribute>> GetAttributesAsync(int id);

        Task AddAttributesAsync(int id, IEnumerable<Attribute> attributes);

        Task DeleteAttributeAsync(int id, string attributeName);
    }
}
