using ABAC.DAL.Entities;
using ABAC.DAL.Repositories.Contracts;
using ABAC.DAL.RuleParser;
using ABAC.DAL.Services.Contracts;
using ABAC.DAL.ViewModels;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ABAC.DAL.Services
{
    public class RuleService : IRuleService
    {
        private IEnumerable<Func<User, Resource, bool>> rules;
        private readonly IEntityRepository<Rule> ruleRepository;
        private readonly IMapper mapper;

        public RuleService(IEntityRepository<Rule> ruleRepository, IMapper mapper)
        {
            LoadRulesAsync().GetAwaiter().GetResult();
            this.ruleRepository = ruleRepository;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<RuleInfo>> GetAsync()
        {
            return mapper.Map<IEnumerable<RuleInfo>>(await ruleRepository.GetAsync());
        }

        public async Task<RuleInfo> GetAsync(int id)
        {
            return mapper.Map<RuleInfo>(await ruleRepository.GetByIdAsync(id));
        }

        public async Task CreateOrUpdateAsync(RuleInfo rule)
        {
            await ruleRepository.CreateOrUpdateAsync(mapper.Map<Rule>(rule));
        }

        public async Task DeleteAsync(int id)
        {
            await ruleRepository.DeleteByIdAsync(id);
        }

        public bool Validate(User user, Resource resource)
        {
            return rules.Any(r => r(user, resource));
        }

        public async Task LoadRulesAsync()
        {
            var ruleStrings = await ruleRepository.GetAsync();
            rules = ruleStrings.Select(r => r.Value).Select(JsonConvert.DeserializeObject<RuleNode>).Select(Build);
        }

        private Func<User, Resource, bool> Build(RuleNode root)
        {
            var expressionString = ParseRuleNode(root);
            var user = Expression.Parameter(typeof(User), "u");
            var resource = Expression.Parameter(typeof(Resource), "r");

            var expression = DynamicExpressionParser.ParseLambda(new[] { user, resource }, typeof(bool), expressionString);

            return (Func<User, Resource, bool>)expression.Compile();
        }

        private string ParseRuleNode(RuleNode node)
        {
            switch (node.Type)
            {
                case NodeType.Single:
                    return ParsePredicate(node.Value);
                case NodeType.And:
                    return $"({string.Join(" && ", node.Children.Select(ParseRuleNode))})";
                case NodeType.Or:
                    return $"({string.Join(" || ", node.Children.Select(ParseRuleNode))})";
                default:
                    throw new ArgumentException("Invalid node type.");
            }
        }

        private string ParsePredicate(Predicate predicate)
        {
            string left, right, @operator = GetOperator(predicate.Operation); ;
            if (predicate.Operation == Operation.StringEqual || predicate.Operation == Operation.StringNotEqual)
            {

                left = GetOperand(predicate.Left);
                right = GetOperand(predicate.Right);
            }
            else
            {
                left = $"double.Parse(\"{GetOperand(predicate.Left)}\")";
                right = $"double.Parse(\"{GetOperand(predicate.Right)}\")";
            }

            return $"{left} {@operator} {right}";
        }

        private string GetOperator(Operation operation)
        {
            switch (operation)
            {
                case Operation.StringEqual:
                case Operation.NumericEqual:
                    return "==";
                case Operation.StringNotEqual:
                case Operation.NumericNotEqual:
                    return "!=";
                case Operation.Greater:
                    return ">";
                case Operation.GreaterOrEqual:
                    return ">=";
                case Operation.Less:
                    return "<";
                case Operation.LessOrEqual:
                    return "<=";
                default:
                    throw new ArgumentException("Invalid operation type.");
            }
        }

        private string GetOperand(string operand)
        {
            if (operand.StartsWith("User."))
            {
                return $"u[\"{operand.Substring(5)}\"]";
            }

            if (operand.StartsWith("Resource."))
            {
                return $"r[\"{operand.Substring(9)}\"]";
            }

            return $"\"{operand}\"";
        }
    }
}
