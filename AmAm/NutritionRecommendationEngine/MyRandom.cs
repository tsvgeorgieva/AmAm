using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NutritionRecommendationEngine
{
    public static class MyRandom
    {
        private static Random random = new Random();
        public static int Next(int max)
        {
            return random.Next(max);
        }

        public static double Next(double min, double max)
        {
            return min + (random.NextDouble() * (max - min));
        }
    }

    public static class Ext
    {
        public static T GetRandomMember<T>(this IEnumerable<T> list)
        {
            return list.ElementAt(MyRandom.Next(list.Count()));
        }
    }
}
