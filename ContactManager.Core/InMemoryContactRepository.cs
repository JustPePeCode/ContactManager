using System.Data.Common;
using System.Runtime.CompilerServices;

namespace ContactManager.Core;

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
    

    public Contact GetById(int id)
    {
    return contacts.First(c=>c.Id==id);
    }
    
    public void Change(Contact updated)
    {
        var index = contacts.FindIndex(c=>c.Id == updated.Id);
        contacts[index]=updated;
    }

    public IReadOnlyList<Contact> GetAll()
    {
        return contacts;
    }
}
