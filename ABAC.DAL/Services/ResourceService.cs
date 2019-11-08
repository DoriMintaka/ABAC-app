﻿using ABAC.DAL.Entities;
using ABAC.DAL.ViewModels;
using ABAC.DAL.Repositories.Contracts;
using ABAC.DAL.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Attribute = ABAC.DAL.Entities.Attribute;

namespace ABAC.DAL.Services
{
    public class ResourceService : IService<ResourceInfo>
    {
        private readonly IEntityRepository<Resource> repository;

        public ResourceService(IEntityRepository<Resource> repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<ResourceInfo>> GetAsync()
        {
            //will be mapped later
            return (await repository.GetAsync()).Select(r => new ResourceInfo(){ Id = r.Id, Name = r.Name, Value = r.Value });
        }

        public async Task<ResourceInfo> GetAsync(int id)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                // throw new NotFoundException
            }
            // will be mapped later
            return new ResourceInfo { Id = resource.Id, Name = resource.Name, Value = resource.Value };
        }

        public async Task UpdateAsync(ResourceInfo model)
        {
            var resource = await repository.GetByIdAsync(model.Id);
            if (resource == null)
            {
                // get new resource from the resource factory
                throw new NotImplementedException();
            }

            resource.Name = model.Name;
            resource.Value = model.Value;
            await repository.CreateOrUpdateAsync(resource);
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                //throw new NotFoundException
            }

            await repository.DeleteByIdAsync(id);
        }

        public async Task<IEnumerable<Attribute>> GetAttributesAsync(int id)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                //throw new NotFoundException
            }

            return resource.Attributes;
        }

        public async Task AddAttributesAsync(int id, IEnumerable<Attribute> attributes)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                //throw new NotFoundException
            }

            resource.Attributes = resource.Attributes.Concat(attributes).Distinct(new AttributeEqualityComparer());
            await repository.CreateOrUpdateAsync(resource);
        }

        public async Task DeleteAttributeAsync(int id, Attribute attribute)
        {
            var resource = await repository.GetByIdAsync(id);
            if (resource == null)
            {
                //throw new NotFoundException
            }

            resource.Attributes = resource.Attributes.Where(a => !new AttributeEqualityComparer().Equals(a, attribute));
            await repository.CreateOrUpdateAsync(resource);
        }
    }
}