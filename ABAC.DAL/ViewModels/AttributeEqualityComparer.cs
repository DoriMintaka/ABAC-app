using System;
using System.Collections.Generic;
using Attribute = ABAC.DAL.Entities.Attribute;

namespace ABAC.DAL.ViewModels
{
    class AttributeEqualityComparer : IEqualityComparer<Attribute>
    {
        public bool Equals(Attribute x, Attribute y)
        {
            return x?.Name == y?.Name && x?.Value == y?.Value;
        }

        public int GetHashCode(Attribute obj)
            => throw new NotImplementedException();
    }
}
