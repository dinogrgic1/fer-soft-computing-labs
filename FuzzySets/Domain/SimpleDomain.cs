using System.Collections.Generic;

namespace FuzzySets.Domain
{
    public class SimpleDomain : Domain
    {
        private readonly int _first;
        private readonly int _last;

        public SimpleDomain(int first, int last)
        {
            _first = first;
            _last = last;

            var elements = new List<DomainElement>();
            for (var i = first; i < last; i++)
            {
                elements.Add(new DomainElement(new[] { i }));
            }

            Elements = elements;
        }

        public override IDomain GetComponent(int idx)
        {
            return this;
        }

        public override int GetNumberOfComponents()
        {
            return 1;
        }

        public int GetFirst()
        {
            return _first;
        }

        public int GetLast()
        {
            return _last;
        }

    }
}