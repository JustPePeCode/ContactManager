namespace ContactManager.CLI;

using ContactManager.Core;

public class SystemConsole : IConsole
{
    public void WriteLine(string message) => Console.WriteLine(message);

    public void Write(string message) => Console.Write(message);

    public string ReadLine() => Console.ReadLine()!;
}
