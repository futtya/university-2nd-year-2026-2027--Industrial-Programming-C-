using lab5csharp;

namespace Laba5.Tests;

public class SweetTests
{
    [Theory]
    [InlineData("Candy")]
    [InlineData("Gingerbread")]
    [InlineData("Chocolate")]
    [InlineData("Marshmallow")]
    public void Constructor_PreservesNameAndSortingParameters(string kind)
    {
        Sweet sweet = CreateSweet(kind);

        Assert.Equal("Тестовая сладость", sweet.Name);
        Assert.Equal(50, sweet.Weight);
        Assert.Equal(12, sweet.sugar);
        Assert.Equal(200, sweet.calories);
        Assert.Equal((50d, 12, 200), sweet.GetSortingParams());
    }

    [Theory]
    [InlineData("Candy", "Конфета")]
    [InlineData("Gingerbread", "Пряник")]
    [InlineData("Chocolate", "Шоколад")]
    [InlineData("Marshmallow", "Зефир")]
    public void GetFullDescription_IncludesKindAndAllParameters(string kind, string label)
    {
        Sweet sweet = CreateSweet(kind);

        Assert.Equal(
            $"=> {label} «Тестовая сладость» | Масса: 50 г | Энергетическая ценность: 200 ккал | Сахар: 12 г",
            sweet.GetFullDescription());
    }

    [Theory]
    [InlineData("Candy", "- Тестовая сладость (50 г)\n")]
    [InlineData("Gingerbread", "- Тестовая сладость [Пряник] (50 г)\n")]
    [InlineData("Chocolate", "- Тестовая сладость [Шоколад] (50 г)\n")]
    [InlineData("Marshmallow", "- Тестовая сладость [Зефир] (50 г)\n")]
    public void GetBriefInfo_FormatsCompositionEntry(string kind, string expected)
    {
        Assert.Equal(expected, CreateSweet(kind).GetBriefInfo());
    }

    private static Sweet CreateSweet(string kind) => kind switch
    {
        "Candy" => new Candy("Тестовая сладость", 50, 12, 200),
        "Gingerbread" => new Gingerbread("Тестовая сладость", 50, 12, 200),
        "Chocolate" => new Chocolate("Тестовая сладость", 50, 12, 200),
        "Marshmallow" => new Marshmallow("Тестовая сладость", 50, 12, 200),
        _ => throw new ArgumentOutOfRangeException(nameof(kind))
    };
}
