using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace FuzzySets.Domain
{
    public abstract class Domain : IDomain
    {
        public IEnumerable<DomainElement> Elements;

        public int GetCardinality()
        {
            return Elements.Count();
        }

        public abstract IDomain GetComponent(int idx);

        public abstract  int GetNumberOfComponents();

        public int IndexOfElement(DomainElement element)
        {
            var list = Elements.ToList();
            return list.IndexOf(element);
        }

        public DomainElement ElementForIndex(int idx)
        {
            return Elements.ElementAt(idx);
        }

        IEnumerator<DomainElement> IEnumerable<DomainElement>.GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return Elements.GetEnumerator();
        }

        public static IDomain IntRange(int lower, int upper)
        {
            return lower > upper ? null : new SimpleDomain(lower, upper);
        }

        public static IDomain Combine(IDomain first, IDomain second)
        {
            if (first == null || second == null)
            {
                return null;
            }
            
            return new CompositeDomain(first as SimpleDomain, second as SimpleDomain);
        }
    }
}