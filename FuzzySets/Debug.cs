using System;
using FuzzySets.Domain;
using FuzzySets.FuzzySet;

namespace FuzzySets
{
    public class Debug
    {
        public static void Print(IDomain domain, string headingText)
        {
            if (domain == null || headingText == null)
            {
                return;
            }

            Console.WriteLine(headingText);
            foreach (var el in domain)
            {
                Console.WriteLine($"Element domene je: {el}");
            }

            Console.WriteLine($"Kardinalitet domene je: {domain.GetCardinality()}");
        }

        public static void Print(IFuzzySet fuzzySet, string headingText)
        {
            if (fuzzySet == null || headingText == null)
            {
                return;
            }

            Console.WriteLine(headingText);
            foreach (var el in fuzzySet.GetDomain())
            {
                Console.WriteLine($"d({el}):{fuzzySet.GetValueAt(el):N6}");
            }
        }
    }
}
