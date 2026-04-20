namespace ContactManager.Tests;

using ContactManager.Core;

public class ContactRepositoryTests
{
    [Fact]
    public void Add_ShouldIncreaseContactCount()
    {
        // 1. Arrange: Set up your objects
        var repository = new InMemoryContactRepository();
        var newContact = new Contact("Alice");
        var newContactTweede = new Contact("Tim");

        // 2. Act: Execute the method you are testing
        repository.Add(newContact);
        repository.Add(newContactTweede);

        var result = repository.GetAll();

        // 3. Assert: Verify the result is what you expected
        Assert.Equal(2, result.Count); // Checks if list count is 1
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Alice", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Tim", result[1].Name);
    }
    [Fact]
    public void NewContact_BeforeBeingAdded_ShouldHaveIdZero()
    {
        // 1. Arrange
        var newContact = new Contact("Alice");
        // 2. Act (None needed - we are checking the state right after creation)
        // 3. Assert
        // This proves that the ID is 0 because the Repository hasn't "stamped" it yet.
        Assert.Equal(0, newContact.Id);
    }

    [Fact]
    public void AddShouldBeAddedIn_repository()
    {
        // 1. Arrange: Set up your objects
        var _repository = new InMemoryContactRepository();
        

        // 2. Act: Execute the method you are testing
        
        var service = new ContactService(_repository);
        service.AddContact("Alice");
        service.AddContact("Tim");

        var result = _repository.GetAll();

        // 3. Assert: Verify the result is what you expected
        Assert.Equal(2, result.Count); // Checks if list count is 1
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Alice", result[0].Name);
        Assert.Equal(2, result[1].Id);
        Assert.Equal("Tim", result[1].Name);
    }

}