using System.Data.Common;

namespace ContactManager.Core;

//Orcestrader
public class ContactService(InMemoryContactRepository _repository)
{
    public void AddContact(string name, string email, string gsmNummer)
    {
        _repository.Add(new Contact(name, email, gsmNummer));
    }
    public void ChangeContact(int id, string name, string? email, string? gsmNummer)
    {
        var contact = _repository.GetById(id);
        contact.Name = name;
        contact.Email = email;
        contact.GsmNummer = gsmNummer;
        _repository.Change(contact);
    }
    public bool IdExists(int id)
    {
        return _repository.idExists(id);
    }
    public IReadOnlyList<Contact> GetContacts()
    {
        return _repository.GetAll();
    }
    public Contact GetContactById(int id)
    {
        return _repository.GetById(id);
    }
    private bool IsValidEmail(string email)
{
    if (string.IsNullOrWhiteSpace(email)) return true; 
    return email.Contains("@") && email.EndsWith(".com", StringComparison.OrdinalIgnoreCase);
}
}
