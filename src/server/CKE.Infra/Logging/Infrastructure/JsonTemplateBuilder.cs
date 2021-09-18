namespace CKE.Infra.Logging.Infrastructure
{
    using System.Collections.Generic;
    using Serilog.Templates;

    public class JsonTemplateBuilder
    {
        private List<string> _fields = new List<string>();

        public JsonTemplateBuilder Add(string field, string expression)
        {
            return Add($"{field}: {expression}");
        }

        public JsonTemplateBuilder Add(string expression)
        {
            _fields.Add(expression);
            return this;
        }

        public ExpressionTemplate Build()
        {
            var expression = $"{{ {{ {string.Join(",", _fields)} }} }}" + "\n";

            return new ExpressionTemplate(expression);
        }
    }
}
