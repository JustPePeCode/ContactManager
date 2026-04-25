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
        contact.Id = nextId;
        nextId++;
        contacts.Add(contact);
    }

    public Contact GetById(int id)
    {
        foreach (var contact in contacts)
        {
            if (id == contact.Id)
            {
                return contact;
            }
        }
        throw new KeyNotFoundException("voer een geldige id in");

        //return contacts.First(c=>c.Id==id);
    }

    public bool idExists(int id)
    {
        return (contacts.FirstOrDefault(contact => contact.Id == id) != null);
    }

    public void Change(Contact updated)
    {
        //var oldContact= contacts.FirstOrDefault(contact =>contact.Id == updated.Id);//alternatieve optie

        var index = contacts.FindIndex(c => c.Id == updated.Id);
        contacts[index] = updated;
        // in de list contacts zoeken we match met "id x" bij de eerst gevonden match updated
    }

    public IReadOnlyList<Contact> GetAll()
    {
        return contacts;
    }

    public void Remove(Contact contact)
    {
        contacts.Remove(contact);
        //var index = contacts.FindIndex(c=>c.Id==removed.Id);
        //contacts.RemoveAt(index);
    }
}
