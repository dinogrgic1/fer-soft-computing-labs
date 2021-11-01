using System.Collections.Generic;
using System.Linq;

namespace FuzzySets.Domain
{
    public class CompositeDomain : Domain
    {
        private SimpleDomain[] _components;

        public CompositeDomain(params SimpleDomain[] domains)
        {
            _components = domains;

            var elements = new List<DomainElement>();
            if (domains.Length > 0)
            {
                elements = domains.First().Elements.ToList();
            }

            domains = domains.Skip(1).ToArray();
            foreach (var domain in domains)
            {
                // Get current cross product in elements
                var newCrossProduct = new List<DomainElement>();
                foreach (var currCrossProduct in elements)
                {
                    foreach (var domainElement in domain.Elements)
                    {
                        var newCrossProductArray = currCrossProduct.Values.ToList();
                        newCrossProductArray.AddRange(domainElement.Values);
                        newCrossProduct.Add(new DomainElement(newCrossProductArray.ToArray()));
                    }
                }
                elements = new List<DomainElement>(newCrossProduct);
            }

            Elements = elements;
        }

        public override IDomain GetComponent(int idx)
        {
            return _components[idx];
        }

        public override int GetNumberOfComponents()
        {
            return _components.Length;
        }
    }
}