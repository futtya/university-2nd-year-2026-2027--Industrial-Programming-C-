namespace lab5csharp
{
    public class Candy : Sweet
    {
        public Candy(string name, double weight, int sugar, int calories) : base(name, weight, sugar, calories) { } 
        
        public override string GetFullDescription()
        {
            return $"=> Конфета «{Name}» | Масса: {Weight} г | Энергетическая ценность: {calories} ккал | Сахар: {sugar} г";
        }

        public override string GetBriefInfo()
        {
            return $"- {Name} ({Weight} г)\n";
        }
    }
}
