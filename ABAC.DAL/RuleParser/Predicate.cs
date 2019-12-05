using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ABAC.DAL.RuleParser
{
    public class Predicate
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public Operation Operation { get; set; }

        public string Left { get; set; }

        public string Right { get; set; }
    }
}
