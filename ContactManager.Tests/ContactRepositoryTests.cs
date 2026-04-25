namespace ContactManager.Tests;

using ContactManager.Core;

public class ContactRepositoryTests
{
    [Fact]
    public void Add_ShouldIncreaseContactCount()
    {
        // 1. Arrange: Set up your objects
        var repository = new InMemoryContactRepository();
        var newContact = new Contact("Spoederman", "spoederman@hotmail.com", "0478125689");
        var newContactTweede = new Contact("Timmy", "Timmy@gmail.com", "");

        // 2. Act: Execute the method you are testing
        repository.Add(newContact);
        repository.Add(newContactTweede);

        var result = repository.GetAll();

        // 3. Assert: Verify the result is what you expected
        Assert.Equal(2, result.Count);
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Spoederman", result[0].Name);
        Assert.Equal("spoederman@hotmail.com", result[0].Email);
        Assert.Equal("0478125689", result[0].GsmNummer);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Timmy", result[1].Name);
        Assert.Equal("Timmy@gmail.com", result[1].Email);
        Assert.Equal("", result[1].GsmNummer);
    }

    [Fact]
    public void NewContact_BeforeBeingAdded_ShouldHaveIdZero()
    {
        // 1. Arrange
        var newContact = new Contact("SpoederMan", "spoederman@hotmail.com", "0478125689");
        // 2. Act (None needed - we are checking the state right after creation)
        // 3. Assert
        // This proves that the ID is 0 because the Repository hasn't "stamped" it yet.
        Assert.Equal(0, newContact.Id);
    }

    [Fact]
    public void GetById_ShouldRetriveAContact()
    {
        var repository = new InMemoryContactRepository();
        var newContact = new Contact("Spoederman", "spoederman@hotmail.com", "0478125689");

        repository.Add(newContact);

        var result = repository.GetById(1);
        Assert.Equal("Spoederman", result.Name);
        Assert.Equal("spoederman@hotmail.com", result.Email);
        Assert.Equal("0478125689", result.GsmNummer);
    }

    [Fact]
    public void GetById_ShouldThrowWhenInvalidId()
    {
        var repository = new InMemoryContactRepository();
        var newContact = new Contact("Spoederman", "spoederman@hotmail.com", "0478125689");

        repository.Add(newContact);

        Assert.Throws<KeyNotFoundException>(() => repository.GetById(2));
    }

    [Fact]
    public void Change_ShouldChangeAContact()
    {
        var repository = new InMemoryContactRepository();
        var newContact = new Contact("Spoederman", "spoederman@hotmail.com", "0478125689");
        var updated = new Contact("peter", "peter@hotmail.com", "12345");
        repository.Add(newContact);
        updated.Id = 1;
        repository.Change(updated);

        var result = repository.GetById(1);

        Assert.Equal("peter", result.Name);
        Assert.Equal("peter@hotmail.com", result.Email);
        Assert.Equal("12345", result.GsmNummer);
    }

    [Fact]
    public void Remove_ShouldRemoveAContact()
    {
        var repository = new InMemoryContactRepository();
        var newContact = new Contact("Spoederman", "spoederman@hotmail.com", "0478125689");

        repository.Add(newContact);

        repository.Remove(newContact);
        var result = repository.GetAll();

        Assert.Equal(0, result.Count);
    }

    [Fact]
    public void idExists_ShouldConfirmIfAnIdExists()
    {
        var repository = new InMemoryContactRepository();
        var newContact = new Contact("Spoederman", "spoederman@hotmail.com", "0478125689");

        repository.Add(newContact);

        var result = repository.idExists(1);

        Assert.True(result);
    }

    [Fact]
    public void idExists_ShouldConfirmIfAnIdDoesntExist()
    {
        var repository = new InMemoryContactRepository();
        var newContact = new Contact("Spoederman", "spoederman@hotmail.com", "0478125689");

        repository.Add(newContact);

        var result = repository.idExists(2);

        Assert.False(result);
    }
}
