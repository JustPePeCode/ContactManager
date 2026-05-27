public class CreateContactRequest
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string GsmNummer { get; set; } = "";
}

public class CreateContactResponse
{
    public int Id { get; set; }
}

public class UpdateContactRequest
{
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string GsmNummer { get; set; } = "";
}

public class ContactResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Email { get; set; } = "";
    public string GsmNummer { get; set; } = "";
}
