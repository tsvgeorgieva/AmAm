using NutritionRecommendationEngine.Engine.Crossover;
using NutritionRecommendationEngine.Engine.Initializer;
using NutritionRecommendationEngine.Engine.Meals;
using NutritionRecommendationEngine.Engine.Mutator;
using System;
using System.Collections.Generic;

namespace NutritionRecommendationEngine.Engine
{
    public class Engine
    {
        public IInitializer PopulationInitializer { get; }
        public ICrossover PopulationCrossoverer { get; }
        public IMutator PopulationMutator { get; }
        public IEnumerable<DietaryReferenceIntake> MaxWeight { get; private set; }
        public int IterationCount { get; private set; }
        public IEnumerable<Food> Items { get; private set; }

        public Engine(IInitializer initializer, ICrossover crossoverer, IMutator mutator)
        {
            PopulationInitializer = initializer;
            PopulationMutator = mutator;
            PopulationCrossoverer = crossoverer;
        }

        public IMeal Solve(IEnumerable<DietaryReferenceIntake> maxWeight, IEnumerable<Food> items, int iterationCount)
        {
            MaxWeight = maxWeight;
            Items = items;
            IterationCount = iterationCount;

            var population = PopulationInitializer.InitializePopulation(MaxWeight, items);

            for (int i = 0; i < IterationCount; i++)
            {
                Console.WriteLine($"{population.GetFittest().TotalCostSum:F2}");
                population.RemoveNotFit();
                PopulationCrossoverer.CrossoverPopulation(population);
                PopulationMutator.MutatePopulation(population);
                population.MergeNewGeneration();
            }

            return population.GetFittest();
        }
    }
}
