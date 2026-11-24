using System;

namespace lab5csharp
{
    public abstract class Sweet
    {
        public string Name;
        public double Weight;
        public int sugar;
        public int calories;

        public Sweet(string Name, double Weight, int sugar, int calories)
        {
            this.Name = Name;
            this.Weight = Weight;
            this.sugar = sugar;
            this.calories = calories;
        }

        public abstract string GetFullDescription();

        public (double weight, int sugar, int calories) GetSortingParams()
        {
            return (Weight, sugar, calories);
        }

        public abstract string GetBriefInfo();
    }
}
