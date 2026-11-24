namespace lab5csharp
{
    public class Chocolate : Sweet
    {
        public Chocolate(string name, double weight, int sugar, int calories) : base(name, weight, sugar, calories) { }

        public override string GetFullDescription()
        {
            return $"=> Шоколад «{Name}» | Масса: {Weight} г | Энергетическая ценность: {calories} ккал | Сахар: {sugar} г";
        }

        public override string GetBriefInfo()
        {
            return $"- {Name} [Шоколад] ({Weight} г)\n";
        }
    }
}
