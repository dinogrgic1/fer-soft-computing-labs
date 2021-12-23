using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public abstract class GenAlgorithm
    {
        protected List<Chromosome> _population;
        private readonly int _genesNum;
        protected readonly int _populationSize;
        
        private readonly int _k;
        protected readonly double _mutationProbability;
        private bool _bias = false;
        
        private readonly Func<double[], double> _function;
        private readonly FunctionData _expected;

        public GenAlgorithm(int genesNum, Func<double[], double> func, FunctionData fd,
            int populationSize = 50, double mutationProbability = 0.0018, bool bias = true, 
            double lowerBound = -4.0D, double upperBound = 4.0D)
        {
            _genesNum = genesNum;
            _populationSize = populationSize;
            
            _bias = bias;
            _mutationProbability = mutationProbability;
            
            _expected = fd;
            _function = func;

            // Prepare
            _population = GeneratePopulation(lowerBound, upperBound);
            CalculateQuadraticMistake(_population);
            _population.Sort();
        }

        protected abstract void Step(int iteration);

        protected abstract List<Chromosome> ChooseParents();

        public void Run(int times)
        {
            for (var i = 1; i <= times; i++)
            {
                Step(i);
            }
        }

        public void Run(double precision)
        {
            var step = 0;
            while (_population.Min()?.Punishment > precision)
            {
                Step(++step);
            }
        }
        private List<Chromosome> GeneratePopulation(double lowerBound, double upperBound)
        {
            var generation = new List<Chromosome>();
            var rand = new Random();
            for (var j = 0; j < _populationSize; j++)
            {
                var values = new List<double>();
                for (var i = 0; i < _genesNum; i++)
                {
                    values.Add(rand.NextDouble() * (upperBound - lowerBound) + lowerBound);
                }
                generation.Add(new Chromosome(values.ToArray())); 
            }
            return generation;
        }

        protected void CalculateQuadraticMistake(List<Chromosome> population)
        {
            foreach (var chromosome in population)
            {
                var values = new List<double>();
                foreach (var (input, expectedOutput) in _expected.Data)
                {
                    var param = new[]
                    {
                        input.ElementAt(0), input.ElementAt(1), chromosome.Value[0], 
                        chromosome.Value[1], chromosome.Value[2], chromosome.Value[3],
                        chromosome.Value[4]
                    };
                    var output = _function.Invoke(param);
                    values.Add(Math.Pow(output - expectedOutput, 2));
                }
                chromosome.SetPunishment(values.Average());
            }
        }
    }
}