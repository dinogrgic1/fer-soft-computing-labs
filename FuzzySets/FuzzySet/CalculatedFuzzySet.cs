using FuzzySets.Domain;
using FuzzySets.Operations;

namespace FuzzySets.FuzzySet
{
    public class CalculatedFuzzySet : IFuzzySet
    {
        private readonly IDomain _domain;
        private readonly IIntUnaryFunction _function;
        
        public CalculatedFuzzySet(IDomain domain, IIntUnaryFunction function)
        {
            _domain = domain;
            _function = function;
        }

        public IDomain GetDomain()
        {
            return _domain;
        }

        public double GetValueAt(DomainElement element)
        {
            var idx = _domain.IndexOfElement(element);
            return _function.ValueAt(idx);
        }
    }
}