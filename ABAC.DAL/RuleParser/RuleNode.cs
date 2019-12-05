using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;

namespace ABAC.DAL.RuleParser
{
    public class RuleNode
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public NodeType Type { get; set; }

        public IEnumerable<RuleNode> Children { get; set; }

        public Predicate Value { get; set; }
    }
}
