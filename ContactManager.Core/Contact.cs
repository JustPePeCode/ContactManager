namespace ContactManager.Core;

public class Contact(int id, string name)
{
    public int Id { get; private set; } = id;
    public string Name { get; private set; } = name;
}

public class InMemoryContactRepository
{
    public void Add(Contact contact)
    {
        contacts.Add(contact);
    }
    public IReadOnlyList<Contact> GetAll()
    {
        return contacts.AsReadOnly();
    }
    private List<Contact> contacts = [];

}