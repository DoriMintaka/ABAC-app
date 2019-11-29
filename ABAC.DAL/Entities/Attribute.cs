using System.ComponentModel.DataAnnotations;

namespace ABAC.DAL.Entities
{
    public class Attribute
    {
        [Key]
        public string Name { get; set; }

        public string Value { get; set; }
    }
}
