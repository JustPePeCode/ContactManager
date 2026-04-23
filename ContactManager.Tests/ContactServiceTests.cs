namespace ContactManager.Tests;

using ContactManager.Core;

public class ContactServiceTests
{
    [Fact]
    public void AddShouldBeAddedIn_repository()
    {
        // 1. Arrange: Set up your objects
        var _repository = new InMemoryContactRepository();

        // 2. Act: Execute the method you are testing

        var service = new ContactService(_repository);
        service.AddContact("Spoederman","spoederman@hotmail.com","0478125689");
        service.AddContact("Timmy","Timmy@gmail.com","");

        var result = _repository.GetAll();

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
}
