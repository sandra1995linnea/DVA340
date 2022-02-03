using System;

namespace TravelingSalesMan_GeneticAlgorithm
{
    interface ICrossover
    {
        Individual CrossoverAndMutate(Individual parent1, Individual parent2, Random random);
    }
}