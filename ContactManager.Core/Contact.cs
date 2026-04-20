using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;

namespace ContactManager.Core;
public interface IConsole
{
    public void WriteLine(string message);
    public void Write(string message);
    public string ReadLine();
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



