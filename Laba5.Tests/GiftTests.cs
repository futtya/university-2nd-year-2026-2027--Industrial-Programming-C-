using lab5csharp;

namespace Laba5.Tests;

[Collection("Console")]
public class GiftTests
{
    [Fact]
    public void ShowGiftComposition_EmptyGift_ReportsNoSweets()
    {
        var gift = new Gift("Пустой подарок");

        Assert.Equal("Нет сладостей", gift.ShowGiftComposition());
    }

    [Fact]
    public void AddDirectly_MixedSweets_AppearInCompositionInInsertionOrder()
    {
        var gift = new Gift("Новогодний");
        gift.AddDirectly(new Candy("Конфета", 10, 3, 40));
        gift.AddDirectly(new Gingerbread("Пряник", 20, 4, 60));
        gift.AddDirectly(new Chocolate("Шоколад", 30, 5, 160));
        gift.AddDirectly(new Marshmallow("Зефир", 40, 6, 130));

        Assert.Equal(
            "Состав подарка: Новогодний\n" +
            "- Конфета (10 г)\n" +
            "- Пряник [Пряник] (20 г)\n" +
            "- Шоколад [Шоколад] (30 г)\n" +
            "- Зефир [Зефир] (40 г)\n",
            gift.ShowGiftComposition());
    }

    [Fact]
    public void CalculateTotalWeight_EmptyGift_PrintsZero()
    {
        using var console = new ConsoleScope();
        var gift = new Gift("Пустой подарок");

        gift.CalculateTotalWeight();

        Assert.Equal($"\nВес подарка: 0 г.{Environment.NewLine}", console.Output);
    }

    [Fact]
    public void CalculateTotalWeight_SumsFractionalWeightsOfDifferentKinds()
    {
        using var console = new ConsoleScope();
        var gift = new Gift("Подарок");
        gift.AddDirectly(new Candy("Конфета", 10.25, 3, 40));
        gift.AddDirectly(new Gingerbread("Пряник", 20.5, 4, 60));
        gift.AddDirectly(new Chocolate("Шоколад", 30.75, 5, 160));
        gift.AddDirectly(new Marshmallow("Зефир", 0, 0, 0));

        gift.CalculateTotalWeight();

        Assert.Equal($"\nВес подарка: 61.5 г.{Environment.NewLine}", console.Output);
    }

    [Theory]
    [InlineData(1, "Конфета", "")]
    [InlineData(2, "Пряник", " [Пряник]")]
    [InlineData(3, "Шоколад", " [Шоколад]")]
    [InlineData(4, "Зефир", " [Зефир]")]
    public void AddNewSweet_CreatesSelectedKindWithEnteredParameters(
        int choice, string label, string category)
    {
        using var console = new ConsoleScope($"{choice}\nТест\n12.5\n3\n45\n1\n");
        var gift = new Gift("Подарок");

        gift.AddNewSweet();
        gift.SortSweets();

        Assert.Equal($"Состав подарка: Подарок\n- Тест{category} (12.5 г)\n",
            gift.ShowGiftComposition());
        Assert.Contains(
            $"=> {label} «Тест» | Масса: 12.5 г | Энергетическая ценность: 45 ккал | Сахар: 3 г",
            console.Output);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(0)]
    [InlineData(5)]
    public void AddNewSweet_UnknownChoice_DoesNotChangeGift(int choice)
    {
        using var console = new ConsoleScope($"{choice}\n");
        var gift = CreateGiftForSorting();
        string before = gift.ShowGiftComposition();

        gift.AddNewSweet();

        Assert.Equal(before, gift.ShowGiftComposition());
    }

    [Theory]
    [InlineData(1, "Пряник", "Зефир", "Шоколад", "Конфета")]
    [InlineData(2, "Шоколад", "Конфета", "Пряник", "Зефир")]
    [InlineData(3, "Зефир", "Шоколад", "Конфета", "Пряник")]
    public void SortSweets_PrintsAllKindsInAscendingOrder(
        int choice, string first, string second, string third, string fourth)
    {
        using var console = new ConsoleScope($"{choice}\n");
        var gift = CreateGiftForSorting();

        gift.SortSweets();

        string[] descriptions = OutputLines(console.Output)
            .Where(line => line.StartsWith("=> "))
            .ToArray();
        string[] expectedNames = [first, second, third, fourth];
        Assert.Equal(expectedNames.Length, descriptions.Length);
        for (int i = 0; i < expectedNames.Length; i++)
        {
            Assert.Contains($"«{expectedNames[i]}»", descriptions[i]);
        }
    }

    [Theory]
    [InlineData("abc", "Неверный ввод!")]
    [InlineData("", "Неверный ввод!")]
    [InlineData("0", "Неверный выбор!")]
    [InlineData("4", "Неверный выбор!")]
    public void SortSweets_InvalidChoice_ReportsErrorAndPreservesComposition(
        string choice, string message)
    {
        using var console = new ConsoleScope($"{choice}\n");
        var gift = CreateGiftForSorting();
        string before = gift.ShowGiftComposition();

        gift.SortSweets();

        Assert.Contains(message, console.Output);
        Assert.DoesNotContain("=> ", console.Output);
        Assert.Equal(before, gift.ShowGiftComposition());
    }

    [Fact]
    public void SortSweets_EmptyGift_PrintsNoSweetDescriptions()
    {
        using var console = new ConsoleScope("1\n");
        var gift = new Gift("Пустой подарок");

        gift.SortSweets();

        Assert.Contains("Отсортировано по весу:", console.Output);
        Assert.DoesNotContain("=> ", console.Output);
    }

    [Theory]
    [InlineData(10, 20, "Нижняя граница,Середина,Верхняя граница")]
    [InlineData(15, 15, "Середина")]
    [InlineData(0, 9, "Меньше диапазона")]
    [InlineData(21, 30, "Больше диапазона")]
    [InlineData(40, 50, "")]
    public void FindBySugar_PrintsOnlyMatchesIncludingBothBoundaries(
        int min, int max, string expectedNames)
    {
        using var console = new ConsoleScope($"{min}\n{max}\n");
        var gift = new Gift("Подарок");
        gift.AddDirectly(new Candy("Меньше диапазона", 10, 9, 40));
        gift.AddDirectly(new Gingerbread("Нижняя граница", 10, 10, 40));
        gift.AddDirectly(new Chocolate("Середина", 10, 15, 40));
        gift.AddDirectly(new Marshmallow("Верхняя граница", 10, 20, 40));
        gift.AddDirectly(new Candy("Больше диапазона", 10, 21, 40));

        gift.FindBySugar();

        string[] lines = OutputLines(console.Output);
        int headerIndex = Array.FindIndex(lines, line =>
            line.StartsWith("Конфеты, содержание сахара"));
        Assert.True(headerIndex >= 0, "В выводе должен быть заголовок результатов поиска.");
        Assert.Equal(expectedNames.Split(',', StringSplitOptions.RemoveEmptyEntries),
            lines.Skip(headerIndex + 1).ToArray());
    }

    [Fact]
    public void FindBySugar_EmptyGift_PrintsHeaderWithoutMatches()
    {
        using var console = new ConsoleScope("0\n100\n");
        var gift = new Gift("Пустой подарок");

        gift.FindBySugar();

        Assert.EndsWith(
            $"Конфеты, содержание сахара которых находится в промежутке от 0 до 100:{Environment.NewLine}",
            console.Output);
    }

    private static Gift CreateGiftForSorting()
    {
        var gift = new Gift("Подарок");
        gift.AddDirectly(new Candy("Конфета", 40, 20, 300));
        gift.AddDirectly(new Gingerbread("Пряник", 10, 30, 400));
        gift.AddDirectly(new Chocolate("Шоколад", 30, 10, 200));
        gift.AddDirectly(new Marshmallow("Зефир", 20, 40, 100));
        return gift;
    }

    private static string[] OutputLines(string output) =>
        output.Split('\n', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
}
