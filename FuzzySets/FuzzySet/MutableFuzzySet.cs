using FuzzySets.Domain;

namespace FuzzySets.FuzzySet
{
    public class MutableFuzzySet : IFuzzySet
    {
        public double[] Memberships;
        private readonly IDomain _domain;

        public MutableFuzzySet(IDomain domain)
        {
            _domain = domain;
            Memberships = new double[domain.GetCardinality()];
        }

        public IDomain GetDomain()
        {
            return _domain;
        }

        public double GetValueAt(DomainElement element)
        {
            var idx = _domain.IndexOfElement(element);
            return Memberships[idx];
        }

        public MutableFuzzySet Set(DomainElement element, double value)
        {
            var idx = _domain.IndexOfElement(element);
            Memberships[idx] = value;
            return this;
        }
    }
}