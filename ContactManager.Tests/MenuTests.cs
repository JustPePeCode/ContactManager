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
        console.Input.Enqueue("Spoederman");
        console.Input.Enqueue("spoederman@hotmail.com");
        console.Input.Enqueue("0478125689");
        console.Input.Enqueue("q");
        menu.Run();
        List<string> expected =
        [
            //menu
            "1. Contact Toevoegen",
            "2. Contact Aanpassen",
            "3. Contact Lijst Weergeven",
            "4. Contact Verwijderen",
            "q. Exit",
            "Maak uw keuze:",
            //1
            "Voer een naam in: ",
            //type "Spoederman" ENTER

            "Voer een email in (mag leeg zijn): ",
            "Voer een gsm nummer in (mag leeg zijn): ",
            "Contact toegevoegd: Spoederman email: spoederman@hotmail.com gsmnummer:0478125689",
            //menu loops
            "1. Contact Toevoegen",
            "2. Contact Aanpassen",
            "3. Contact Lijst Weergeven",
            "4. Contact Verwijderen",
            "q. Exit",
            "Maak uw keuze:",
            //q
        ];
        Assert.Equal(expected, console.Output);
        var contact = repository.GetAll()[0];
        Assert.Equal(1, contact.Id);
        Assert.Contains("Spoederman", contact.Name);
    }

    [Fact]
    public void HandleChangeContact_NoContacts_ShowsMessage()
    {
        console.Input.Enqueue("2");
        console.Input.Enqueue("q");

        menu.Run();

        Assert.Contains("Geen contacten gevonden, voeg eerst contacten toe!", console.Output);
    }

    [Fact]
    public void HandleChangeContact_ValidId_UpdatesContact()
    {
        service.AddContact("Elvis", "Elvis@mail.com", "0123456789");

        console.Input.Enqueue("2");
        console.Input.Enqueue("1");
        console.Input.Enqueue("Elvis Presley");
        console.Input.Enqueue("ElvisPresley@mail.com");
        console.Input.Enqueue("9876543210");
        console.Input.Enqueue("q");

        menu.Run();

        var contact = repository.GetById(1);
        Assert.Equal("Elvis Presley", contact.Name);
        Assert.Equal("ElvisPresley@mail.com", contact.Email);
        Assert.Equal("9876543210", contact.GsmNummer);
    }

    [Fact]
    public void HandleChangeContact_EmptyInput_KeepsOrignalValue()
    {
        service.AddContact("Elvis", "Elvis@mail.com", "0123456789");

        console.Input.Enqueue("2");
        console.Input.Enqueue("1");
        console.Input.Enqueue("");
        console.Input.Enqueue("");
        console.Input.Enqueue("");
        console.Input.Enqueue("q");

        menu.Run();

        var contact = repository.GetById(1);

        Assert.Equal("Elvis", contact.Name);
        Assert.Equal("Elvis@mail.com", contact.Email);
        Assert.Equal("0123456789", contact.GsmNummer);
    }

    [Fact]
    public void HandleChoice_InvalidOption_ShouldShowErrorMessage()
    {
        // Given
        console.Input.Enqueue("5");
        console.Input.Enqueue("q");
        // When
        menu.Run();

        // Then
        Assert.Contains("Ongeldige optie.", console.Output);
    }

    [Fact]
    public void HandleShowContactList_WhenEmpty_ShouldShowEmptyMessage()
    {
        //Arrange
        console.Input.Enqueue("3");
        console.Input.Enqueue("q");

        //Act
        menu.Run();

        //Assert
        Assert.Contains("Contacten lijst is leeg, voeg contacten toe!", console.Output);
    }

    [Fact]
    public void HandleShowContactList_ShouldDisplayContacts()
    {
        //Arrange
        service.AddContact("Elvis", "Elvis@mail.com", "0123456789");
        console.Input.Enqueue("3");
        console.Input.Enqueue("q");

        //Act
        menu.Run();

        //Assert
        Assert.Contains("-----------------------", console.Output);
        Assert.Contains("Elvis", console.Output);
    }

    [Fact]
    public void HandleRemoveContact_WhenEmpty_ShouldShowEmptyMessage()
    {
        //Arrange
        console.Input.Enqueue("4");
        console.Input.Enqueue("q");

        //Act
        menu.Run();

        //Assert
        Assert.Contains("Geen contacten gevonden, voeg eerst contacten toe!", console.Output);
    }

    [Fact]
    public void HandleRemoveContact_ConfirmWithJ_ShouldRemoveContact()
    {
        service.AddContact("Elvis", "Elvis@mail.com", "0123456789");
        console.Input.Enqueue("4");
        console.Input.Enqueue("1");
        console.Input.Enqueue("J");
        console.Input.Enqueue("q");

        //Act
        menu.Run();
        Assert.Empty(service.GetContacts());
    }

    [Fact]
    public void HandleRemoveContact_ConfirmWithN_ShouldCancelRemoval()
    {
        service.AddContact("Elvis", "Elvis@mail.com", "0123456789");
        console.Input.Enqueue("4");
        console.Input.Enqueue("1");
        console.Input.Enqueue("N");
        console.Input.Enqueue("q");

        //Act
        menu.Run();
        //Assert
        Assert.Contains("Verwijderen geannuleerd", console.Output);
        Assert.Single(service.GetContacts());
    }
}
