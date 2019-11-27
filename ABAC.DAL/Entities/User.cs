using System.Collections.Generic;

namespace ABAC.DAL.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public ICollection<Attribute> Attributes { get; set; }
    }
}
