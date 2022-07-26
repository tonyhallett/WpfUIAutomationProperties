using System;

namespace CodeGen
{
    public class ExplainedType
    {
        public Type Type { get; set; }
        public string? Explanation { get; set; }

        public ExplainedType(Type type, string? explanation)
        {
            Type = type;
            Explanation = explanation;
        }

        public static ExplainedType ToExplain(Type type)
        {
            return new ExplainedType(type, null);
        }
    }

}
