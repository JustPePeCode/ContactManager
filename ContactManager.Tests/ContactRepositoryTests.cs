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
        Assert.Equal(2, result.Count); // Checks if list count is 1
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
}
