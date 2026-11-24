using System.Globalization;

namespace Laba5.Tests;

// Console.In и Console.Out общие для всего процесса, поэтому тесты идут последовательно.
[CollectionDefinition("Console", DisableParallelization = true)]
public class ConsoleCollection
{
}

internal sealed class ConsoleScope : IDisposable
{
    private readonly TextReader originalInput = Console.In;
    private readonly TextWriter originalOutput = Console.Out;
    private readonly CultureInfo originalCulture = CultureInfo.CurrentCulture;
    private readonly StringReader input;
    private readonly StringWriter output = new(CultureInfo.InvariantCulture);

    public ConsoleScope(string inputText = "")
    {
        input = new StringReader(inputText);
        CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
        Console.SetIn(input);
        Console.SetOut(output);
    }

    public string Output => output.ToString();

    public void Dispose()
    {
        Console.SetIn(originalInput);
        Console.SetOut(originalOutput);
        CultureInfo.CurrentCulture = originalCulture;
        input.Dispose();
        output.Dispose();
    }
}
