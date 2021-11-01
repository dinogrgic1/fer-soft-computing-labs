using System;
using FuzzySets.Domain;
using FuzzySets.FuzzySet;

namespace FuzzySets
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Primjer0();
            Primjer1();
            Primjer2();
        }

        public static void Primjer0()
        {
            var d1 = Domain.Domain.IntRange(0, 5);
            Debug.Print(d1, "Elementi domene d1: ");

            var d2 = Domain.Domain.IntRange(0, 3);
            Debug.Print(d2, "Elementi domene d2: ");

            var d3 = Domain.Domain.Combine(d1, d2);
            Debug.Print(d3, "Elementi domene d3: ");

            Console.WriteLine(d3.ElementForIndex(0));
            Console.WriteLine(d3.ElementForIndex(5));
            Console.WriteLine(d3.ElementForIndex(14));
            Console.WriteLine(d3.IndexOfElement(DomainElement.Of(4, 1)));
        }

        public static void Primjer1()
        {
            var d = Domain.Domain.IntRange(0, 11);
            var set1 = new MutableFuzzySet(d)
                .Set(DomainElement.Of(0), 1.0)
                .Set(DomainElement.Of(1), 0.8)
                .Set(DomainElement.Of(2), 0.6)
                .Set(DomainElement.Of(3), 0.4)
                .Set(DomainElement.Of(4), 0.2);
            Debug.Print(set1, "Set1:");

            var d2 = Domain.Domain.IntRange(-5, 6); // {-5,-4,...,4,5}
            var set2 = new CalculatedFuzzySet(d2,
                 StandardFuzzySets.LambdaFunction(
                    d2.IndexOfElement(DomainElement.Of(-4)),
                    d2.IndexOfElement(DomainElement.Of(0)),
                    d2.IndexOfElement(DomainElement.Of(4))
                )
            );

            Debug.Print(set2, "Set2:");
        }

        public static void Primjer2()
        {
            IDomain d = Domain.Domain.IntRange(0, 11);
            IFuzzySet set1 = new MutableFuzzySet(d)
                .Set(DomainElement.Of(0), 1.0)
                .Set(DomainElement.Of(1), 0.8)
                .Set(DomainElement.Of(2), 0.6)
                .Set(DomainElement.Of(3), 0.4)
                .Set(DomainElement.Of(4), 0.2);
            Debug.Print(set1, "Set1:");
            IFuzzySet notSet1 = Operations.Operations.UnaryOperation(set1, Operations.Operations.ZadehNot());
            Debug.Print(notSet1, "notSet1:");
            IFuzzySet union = Operations.Operations.BinaryOperation(set1, notSet1, Operations.Operations.ZadehOr());
            Debug.Print(union, "Set1 union notSet1:");
            IFuzzySet hinters = Operations.Operations.BinaryOperation(set1, notSet1, Operations.Operations.HamacherTNorm(1.0));
            Debug.Print(hinters, "Set1 intersection with notSet1 using parameterised Hamacher T norm with parameter 1.0:");
        }
    }
}
