using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace ContactManager.Core;

public interface IConsole
{
    void WriteLine(string message);
    void Write(string message);
    string? ReadLine();
}

public class Menu(IConsole console, ContactService service) // Menu class that takes a console and service as input (primary constructor)
{
    public int Run() // Starts the menu loop, returns 0 when done (0 = success)
    {
        var running = true; // As long as this is true, keep showing the menu
        while (running) // Keep looping until running becomes false
        {
            ShowMenu(); // Print the menu options to the console
            running = HandleChoice(console.ReadLine()); // Read what the user typed, handle it, and update running
        }
        return 0; // Loop ended, return 0 to signal the program exited cleanly
    }

    private void ShowMenu() // Prints the available menu options
    {
        console.WriteLine("1. Contact Toevoegen");
        console.WriteLine("q. Exit"); // Print "q. Exit" as a menu option
        console.Write("Maak uw keuze:"); // Print the prompt (no newline, cursor stays on same line)
    }

    private bool HandleChoice(string choice) // Receives what the user typed, returns true to keep looping or false to stop
    {
        switch (choice) // Check what the user typed
        {
            case "1":
                HandleAddContact();
                break;
            case "q":
                return false; // User typed "q" → return false → running becomes false → loop stops
            default:
                console.WriteLine("Ongeldige optie.");
                break; // Anything else → print error → break out of switch
        }
        return true; // Return true → running stays true → loop continues
    }

    private void HandleAddContact()
    {
        console.Write("Voer een naam in: "); // Prompt the user
        var name = console.ReadLine(); // Read the name they type
        service.AddContact(name); // Save it via the service
        console.WriteLine($"Contact toegevoegd: {name}"); // Confirm it was added
    }
}

//Domein
public class Contact(string name)
{
    public int Id { get; set; }
    public string Name { get; private set; } = name;
}

//Infrasstrucuur
public class InMemoryContactRepository()
{
    private List<Contact> contacts = [];
    private int nextId = 1;

    public void Add(Contact contact)
    {
        contacts.Add(contact);
        contact.Id = nextId;
        nextId++;
    }

    public IReadOnlyList<Contact> GetAll()
    {
        return contacts;
    }
}

//Orcestrader
public class ContactService(InMemoryContactRepository _repository)
{
    public void AddContact(string name)
    {
        _repository.Add(new Contact(name));
    }
}
