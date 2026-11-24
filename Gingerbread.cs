namespace lab5csharp
{
    public class Gingerbread : Sweet
    {
        public Gingerbread(string name, double weight, int sugar, int calories) : base(name, weight, sugar, calories) { }

        public override string GetFullDescription()
        {
            return $"=> Пряник «{Name}» | Масса: {Weight} г | Энергетическая ценность: {calories} ккал | Сахар: {sugar} г";
        }

        public override string GetBriefInfo()
        {
            return $"- {Name} [Пряник] ({Weight} г)\n";
        }
    }
}
