using FuzzySets.Domain;

namespace FuzzySets.FuzzySet
{
    public interface IFuzzySet
    {
        public IDomain GetDomain();
        public double GetValueAt(DomainElement element);
    }
}