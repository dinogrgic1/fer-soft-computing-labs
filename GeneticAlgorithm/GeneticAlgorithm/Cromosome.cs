using System;
using MathNet.Numerics.Distributions;

namespace GeneticAlgorithm
{
    public class Chromosome : IComparable<Chromosome>
    {
        public double[] Value { get; }
        public double Punishment { get; private set; }

        public Chromosome(double[] values)
        {
            Value = values;
        }

        public void SetPunishment(double fitness)
        {
            Punishment = fitness;
        }

        public void Mutate(double p)
        {
            var random = new Random();
            for (var i = 0; i < Value.Length; i++)
            {
                if (!(random.NextSingle() < p))
                {
                    continue;
                }
                var normalDist = new Normal(0, 1);
                var randomGaussianValue = normalDist.Sample() * 0.5;
                Value[i] += randomGaussianValue;
            }
        }

        public override string ToString()
        {
            var strArr = "";
            foreach (var val in Value)
            {
                strArr += $"{val:0.000000} ";
            }
            strArr = strArr.Trim();
            return $"({strArr}) | punishment: {Punishment}";
        }

        public static Chromosome Crossover(Chromosome first, Chromosome second)
        {
            var values = new double[first.Value.Length];
            for (var i = 0; i < first.Value.Length; i++)
            {
                values[i] = (first.Value[i] + second.Value[i]) / 2;
            }
            return new Chromosome(values);
        }
        
        public int CompareTo(Chromosome? other)
        {
            if (ReferenceEquals(this, other))
            {
                return 0;
            }

            if (ReferenceEquals(null, other))
            {
                return 1;
            }

            return Punishment.CompareTo(other.Punishment);
        }
    }
}