using System.Collections.Generic;

namespace FuzzySets.Domain
{
    public interface IDomain : IEnumerable<DomainElement>
    {
        public int GetCardinality();
        public IDomain GetComponent(int idx);
        public int GetNumberOfComponents();
        public int IndexOfElement(DomainElement element);
        public DomainElement ElementForIndex(int idx);
    }
}