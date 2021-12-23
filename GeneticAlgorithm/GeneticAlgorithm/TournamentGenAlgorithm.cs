using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm
{
    public class TournamentGenAlgorithm : GenAlgorithm
    {
        private readonly int _k;
        public TournamentGenAlgorithm(int genesNum, Func<double[], double> function, FunctionData expected, int k = 3, 
            int populationSize = 50, double mutationProbability = 0.002, bool bias = true, 
            double lowerBound = -4.0D, double upperBound = 4.0D) : 
            base(genesNum, function, expected, populationSize, mutationProbability, bias, lowerBound, upperBound)
        {
            _k = k;
        }
        
        protected override void Step(int iteration)
        {
            var newGeneration = new List<Chromosome>(_population);
            var select = ChooseParents();
            var worst = select[_k-1];
            select.Remove(worst);
            
            var child = Chromosome.Crossover(select[0], select[1]);
            child.Mutate(_mutationProbability);
            
            newGeneration.Remove(worst);
            newGeneration.Add(child);
            
            _population = new List<Chromosome>(newGeneration);
            CalculateQuadraticMistake(new List<Chromosome>(){child});
            _population.Sort();
            Console.WriteLine($"Iteration #{iteration} {_population.Min()}");
        }

        protected override List<Chromosome> ChooseParents()
        {
            if (_k > _populationSize)
            {
                throw new Exception("K cannot be larger then population size");
            }

            var rand = new Random();
            var parentsIdx = new HashSet<int>();
            while (parentsIdx.Count != _k)
            {
                parentsIdx.Add(rand.Next(0, _populationSize));
            }

            var parents = parentsIdx.Select(idx => _population[idx]).ToList();
            parents.Sort();
            return parents;      
        }
    }
}