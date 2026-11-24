namespace lab5csharp
{
    public class Marshmallow : Sweet
    {
        public Marshmallow(string name, double weight, int sugar, int calories) : base(name, weight, sugar, calories) { }

        public override string GetFullDescription()
        {
            return $"=> Зефир «{Name}» | Масса: {Weight} г | Энергетическая ценность: {calories} ккал | Сахар: {sugar} г";
        }

        public override string GetBriefInfo()
        {
            return $"- {Name} [Зефир] ({Weight} г)\n";
        }
    }
}
