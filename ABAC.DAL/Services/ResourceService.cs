using ABAC.DAL.Entities;
using ABAC.DAL.Exceptions;
using ABAC.DAL.Extensions;
using ABAC.DAL.Repositories.Contracts;
using ABAC.DAL.Services.Contracts;
using ABAC.DAL.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attribute = ABAC.DAL.Entities.Attribute;

namespace ABAC.DAL.Services
{
    public class ResourceService : IService<ResourceInfo>
    {
        private readonly IEntityRepository<Resource> repository;
        private readonly IMapper mapper;

        public ResourceService(IEntityRepository<Resource> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ResourceInfo>> GetAsync()
        {
            return (await repository.GetAsync()).Select(r => mapper.Map<ResourceInfo>(r));
        }

        public async Task<ResourceInfo> GetAsync(int id)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                throw new NotFoundException();
            }

            return mapper.Map<ResourceInfo>(resource);
        }

        public async Task UpdateAsync(ResourceInfo model)
        {
            var resource = await repository.GetByIdAsync(model.Id) ?? new Resource().SetDefaultAttributes();

            resource.SetInfo(model);
            await repository.CreateOrUpdateAsync(resource);
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                throw new NotFoundException();
            }

            await repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<Attribute>> GetAttributesAsync(int id)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                throw new NotFoundException();
            }

            return resource.Attributes.Select(kvp => mapper.Map<Attribute>(kvp));
        }

        public async Task AddAttributesAsync(int id, IEnumerable<Attribute> attributes)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                throw new NotFoundException();
            }

            foreach (var attribute in attributes)
            {
                resource.Attributes.Add(attribute);
            }

            await repository.CreateOrUpdateAsync(resource);
        }

        public async Task DeleteAttributeAsync(int id, string attributeName)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                throw new NotFoundException();
            }

            var attribute = resource.Attributes.SingleOrDefault(a => a.Name == attributeName);

            if (attribute == null)
            {
                throw new NotFoundException();
            }

            resource.Attributes.Remove(attribute);

            await repository.CreateOrUpdateAsync(resource);
        }
    }
}
