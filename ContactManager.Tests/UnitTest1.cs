namespace ContactManager.Tests;
using ContactManager.Core;

public class ContactRepositoryTests
{
    [Fact]
    public void Add_ShouldIncreaseContactCount()
    {
        // 1. Arrange: Set up your objects
        var repository = new InMemoryContactRepository();
        var newContact = new Contact(1, "Alice");

        // 2. Act: Execute the method you are testing
        repository.Add(newContact);
        var result = repository.GetAll();

        // 3. Assert: Verify the result is what you expected
        Assert.Single(result); // Checks if list count is 1
        Assert.Equal(1, result[0].Id);
        Assert.Equal("Alice", result[0].Name);
    }
}