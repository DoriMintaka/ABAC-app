using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ABAC.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public ICollection<Attribute> Attributes { get; set; } = new List<Attribute>();

        [NotMapped]
        public string this[string index]
        {
            get
            {
                return Attributes.SingleOrDefault(a => a.Name == index)?.Value;
            }
            set
            {
                var temp = Attributes.SingleOrDefault(a => a.Name == index);
                if (temp != null)
                {
                    Attributes.Remove(temp);
                }

                Attributes.Add(new Attribute { Name = index, Value = value });
            }
        }
    }
}
