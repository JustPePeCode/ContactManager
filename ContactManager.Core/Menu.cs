namespace ContactManager.Core;

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
        console.WriteLine("2. Contact Aanpassen");
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
            case "2":
                HandleChangeContact();
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
        console.Write("Voer een email in (mag leeg zijn): "); // Prompt the user
        var email = console.ReadLine(); // Read the name they type
        console.Write("Voer een gsm nummer in (mag leeg zijn): "); // Prompt the user
        var gsmNummer = console.ReadLine(); // Read the name they type
        service.AddContact(name, email, gsmNummer); // Save it via the service
        console.WriteLine($"Contact toegevoegd: {name} email: {email} gsmnummer:{gsmNummer}"); // Confirm it was added
    }
    private void HandleChangeContact()
    {

        var id = 0;
        while (service.IdExists(id))
        {
            console.Write("Welke Contact wilt u aanpassen (voer contact ID in)");
            id = int.Parse(console.ReadLine());
            if (!service.IdExists(id))
            {
               break; 
            }
            console.WriteLine("Ongeldige Id.");
            

            
        }

        console.Write("Voer nieuwe Naam in: ");
        var newName = console.ReadLine();
        console.Write("Voer nieuwe Email in: ");
        var newEmail = console.ReadLine();
        console.Write("Voer nieuwe GsmNummer in: ");
        var newGsmNummer = console.ReadLine();
        service.ChangeContact(id, newName, newEmail, newGsmNummer);
        console.WriteLine($"Contact aangepast: {newName} email: {newEmail} gsmnummer:{newGsmNummer}");


    }


}
