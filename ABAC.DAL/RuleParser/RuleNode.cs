using System.Collections.Generic;

namespace ABAC.DAL.RuleParser
{
    public class RuleNode
    {
        public NodeType Type { get; set; }

        public IEnumerable<RuleNode> Children { get; set; }

        public Predicate Value { get; set; }
    }
}
