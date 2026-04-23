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
     public bool idExists(int id)
    {
      return (contacts.FirstOrDefault(contact =>contact.Id == id)== null);
    }
    
    
    public void Change(Contact updated)
    {
        var oldContact= contacts.FirstOrDefault(contact =>contact.Id == updated.Id);//alternatieve optie

        var index = contacts.FindIndex(c=>c.Id == updated.Id);
        contacts[index]=updated;
        // in de list contacts zoeken we match met "id x" bij de eerst gevonden match updated
    }

    public IReadOnlyList<Contact> GetAll()
    {
        return contacts;
    }
}
