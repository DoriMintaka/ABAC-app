using System.ComponentModel.DataAnnotations;

namespace ABAC.DAL.Entities
{
    public class Attribute
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Value { get; set; }
    }
}
