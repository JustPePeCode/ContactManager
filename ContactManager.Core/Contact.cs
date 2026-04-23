

namespace ContactManager.Core;

//Domein
public class Contact(string name, string? email, string? gsmNummer)
{
    public int Id { get; set; }
    public string Name { get; set; } = name;
    public string? Email { get; set; } = email;
    public string? GsmNummer { get; set; } = gsmNummer;
}
