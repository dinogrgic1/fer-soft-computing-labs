using System;
using System.Collections.Generic;
using System.Linq;
using FuzzySets.FuzzySet;

namespace FuzzySets.Operations
{
    public class Operations
    {
        private class ZADEH_AND : IBinaryFunction
        {
            public double ValueAt(double first, double second)
            {
                return Math.Min(first, second);
            }
        }

        private class ZADEH_OR : IBinaryFunction
        {
            public double ValueAt(double first, double second)
            {
                return Math.Max(first, second);
            }
        }

        private class ZADEH_NOT : IUnaryFunction
        {
            public double ValueAt(double value)
            {
                return 1 - value;
            }
        }

        private class HAMACHER_T_NORM : IBinaryFunction
        {
            private double _v;

            public HAMACHER_T_NORM(double v)
            {
                _v = v;
            }

            public double ValueAt(double first, double second)
            {
                return (first * second)
                       / (_v + (1 - _v) * (first + second - first * second));
            }
        }

        private class HAMACHER_S_NORM : IBinaryFunction
        {
            private double _v;

            public HAMACHER_S_NORM(double v)
            {
                _v = v;
            }
            public double ValueAt(double first, double second)
            {
                return (first + second - (2 - _v) * first * second)
                       / (1 - (1 - _v) * first * second);
            }
        }

        public static IFuzzySet UnaryOperation(IFuzzySet set, IUnaryFunction function)
        {
            var domain = set.GetDomain();

            var newSet = new MutableFuzzySet(domain);
            var newSetMemberships = new List<double>();

            foreach (var element in domain)
            {
                var elementValue = set.GetValueAt(element);
                newSetMemberships.Add(function.ValueAt(elementValue));
            }

            newSet.Memberships = newSetMemberships.ToArray();
            return newSet;
        }

        public static IFuzzySet BinaryOperation(IFuzzySet first, IFuzzySet second, IBinaryFunction function)
        {
            var domain1 = first.GetDomain();
            var domain2 = second.GetDomain();

            var newSet = new MutableFuzzySet(domain1);
            var newSetMemberships = new List<double>();

            foreach (var element in domain1.Zip(domain2))
            {
                var valueOne = first.GetValueAt(element.First);
                var valueTwo = second.GetValueAt(element.Second);
                newSetMemberships.Add(function.ValueAt(valueOne, valueTwo));
            }

            newSet.Memberships = newSetMemberships.ToArray();
            return newSet;
        }

        public static IUnaryFunction ZadehNot()
        {
            return new ZADEH_NOT();
        }

        public static IBinaryFunction ZadehAnd()
        {
            return new ZADEH_AND();
        }

        public static IBinaryFunction ZadehOr()
        {
            return new ZADEH_OR();
        }

        public static IBinaryFunction HamacherTNorm(double v)
        {
            return new HAMACHER_T_NORM(v);
        }

        public static IBinaryFunction HamacherSNorm(double v)
        {
            return new HAMACHER_S_NORM(v);
        }
    }
}