using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class GenerativeGenAlgorithm : GenAlgorithm
    {
        public GenerativeGenAlgorithm(int genesNum, Func<double[], double> function, FunctionData expected,
            int populationSize = 50, double mutationProbability = 0.002, bool bias = true, 
            double lowerBound = -4.0D, double upperBound = 4.0D) : 
            base(genesNum, function, expected, populationSize, mutationProbability, bias, lowerBound, upperBound) { }
        
        protected override void Step(int iteration)
        {
            // two best BIAS
            var newGeneration = new List<Chromosome>(_population);
            for (var j = 1; j < _populationSize / 2; j++)
            {
                var parent1 = ChooseParents().First();
                var parent2 = ChooseParents().First();
                var child = Chromosome.Crossover(parent1, parent2);
                
                newGeneration[2 * j] = child;
                newGeneration[2 * j].Mutate(_mutationProbability);
                
                CalculateQuadraticMistake(new List<Chromosome>(){newGeneration[2 * j]});
            }
            _population = new List<Chromosome>(newGeneration);
            _population.Sort();
            Console.WriteLine($"Iteration #{iteration} {_population.Min()}");
        }

        protected override List<Chromosome> ChooseParents()
        {
            var random = new Random();
            return new List<Chromosome> {_population[random.Next(0, _populationSize - 1)]};
        }
    }
}