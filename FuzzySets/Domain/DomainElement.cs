using System.Linq;

namespace FuzzySets.Domain
{
    public class DomainElement
    {
        public int[] Values;

        public DomainElement(int[] values)
        {
            Values = values;
        }
        public static DomainElement Of(params int[] values)
        {
            return new(values);
        }

        public override string ToString()
        {
            if (Values.Length > 1)
            {
                return $"({string.Join(",", Values)})";
            }
            return $"{string.Join(",", Values)}";
        }

        public override bool Equals(object obj)
        {
            if (obj is not DomainElement element)
            {
                return false;
            }

            return Values.SequenceEqual(element.Values);
        }

        public override int GetHashCode()
        {
            return (Values != null ? Values.GetHashCode() : 0);
        }
    }
}
