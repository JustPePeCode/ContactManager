namespace ContactManager.Tests;

using ContactManager.Core;

public class FakeConsole : IConsole
{
    public List<string> Output = new();
    public Queue<string> Input = new();

    public void WriteLine(string message) => Output.Add(message);

    public void Write(string message) => Output.Add(message);

    public string ReadLine() => Input.Dequeue(); // Throws if empty
}

public class MenuTests
{
    private ContactService service = new(new InMemoryContactRepository());
    private FakeConsole console = new();

    private Menu menu;

    public MenuTests()
    {
        menu = new Menu(console, service);
    }

    [Fact]
    public void Menu_Q_Exits()
    {
        console.Input.Enqueue("q"); // Simulate user typing "q"
        Assert.Equal(0, menu.Run()); // Check it returns 0 (success)
        Assert.Contains("q. Exit", console.Output); // Check the menu was displayed
    }
}

public class AddContactMenuTests
{
    private readonly InMemoryContactRepository repository = new();
    private readonly ContactService service;
    private readonly FakeConsole console = new();
    private readonly Menu menu;

    public AddContactMenuTests()
    {
        service = new ContactService(repository);
        menu = new Menu(console, service);
    }

    [Fact]
    public void Menu_AddContact_Flow()
    {
        console.Input.Enqueue("1");
        console.Input.Enqueue("Elvis");
        console.Input.Enqueue("q");
        menu.Run();
        List<string> expected =
        [
            //menu
            "1. Contact Toevoegen",
            "q. Exit",
            "Maak uw keuze:",
            //1
            "Voer een naam in: ",
            //type "Elvis" ENTER
            "Contact toegevoegd: Elvis",
            //menu loops
            "1. Contact Toevoegen",
            "q. Exit",
            "Maak uw keuze:",
            //q
        ];
        Assert.Equal(expected, console.Output);
        var contact = repository.GetAll()[0];
        Assert.Equal(1, contact.Id);
        Assert.Contains("Elvis", contact.Name);
    }
}
