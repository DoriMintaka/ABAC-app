using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ABAC.DAL.Entities
{
    public class Resource
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }

        public ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

        [NotMapped]
        public string this[string key]
        {
            get
            {
                return Attributes.SingleOrDefault(a => a.Name == key.ToLowerInvariant())?.Value;
            }
            set
            {
                var temp = Attributes.SingleOrDefault(a => a.Name == key.ToLowerInvariant());
                if (temp != null)
                {
                    Attributes.Remove(temp);
                }

                Attributes.Add(new Attribute { Name = key.ToLowerInvariant(), Value = value.ToLowerInvariant() });
            }
        }
    }
}