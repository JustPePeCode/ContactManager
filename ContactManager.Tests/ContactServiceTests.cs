namespace ContactManager.Tests;

using ContactManager.Core;

public class ContactServiceTests
{
    private InMemoryContactRepository _repository;
    private ContactService _service;

    public ContactServiceTests()
    {
        _repository = new InMemoryContactRepository();
        _service = new ContactService(_repository);
    }

    [Fact]
    public void AddShouldBeAddedIn_repository()
    {
        _service.AddContact("Spoederman", "spoederman@hotmail.com", "0478125689");
        _service.AddContact("Timmy", "Timmy@gmail.com", "");

        var result = _service.GetContacts();

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

    // AAA Test Pattern:
    // Arrange  → set up objects and data needed for the test
    // Act      → call the method you are testing, store the result
    // Assert   → verify the result matches what you expected

    [Fact]
    public void GetContacts_WhenEmpty_ShouldReturnEmptyList()
    {
        // Arrange - nothing needed, repo is empty

        // Act
        var result = _service.GetContacts();

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetContacts_ShouldReturnAllContacts()
    {
        _service.AddContact("Spoederman", "spoederman@hotmail.com", "0478125689");
        _service.AddContact("Timmy", "Timmy@gmail.com", "");

        var result = _service.GetContacts();

        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetContactById_ValidId_ShouldReturnCorrectContact()
    {
        //Arrange
        _service.AddContact("Spoederman", "spoederman@hotmail.com", "0478125689");
        //Act
        var result = _service.GetContactById(1);
        //Assert
        Assert.Equal("Spoederman", result.Name);
    }

    [Fact]
    public void GetContactById_InvalidId_ShouldThrowKeyNotFoundException()
    {
        //Arrange not needed

        //Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _service.GetContactById(420));
    }

    [Fact]
    public void IdExists_ExistingId_ShouldReturnTrue()
    {
        //Arranger
        _service.AddContact("Spoederman", "spoederman@hotmail.com", "0478125689");
        //Act
        var result = _service.IdExists(1);
        //Assert
        Assert.True(result);
    }

    [Fact]
    public void IdExists_NonExistingId_ShouldReturnFalse()
    {
        //Arranger not needed repo is empty

        //Act
        var result = _service.IdExists(1);
        //Assert
        Assert.False(result);
    }

    [Fact]
    public void ChangeContact_ValidId_ShouldUpdateAllFields()
    {
        //Arranger
        _service.AddContact("Spoederman", "spoederman@hotmail.com", "0478125689");
        //Act
        _service.ChangeContact(1, "Spoedervrouw", "spoedervrouw@hotmail.com", "011111111");
        var result = _service.GetContactById(1);
        //Assert
        Assert.Equal("Spoedervrouw", result.Name);
        Assert.Equal("spoedervrouw@hotmail.com", result.Email);
        Assert.Equal("011111111", result.GsmNummer);
    }

    [Fact]
    public void ChangeContact_WithNullEmailAndGsm_ShouldUpdateToNull()
    {
        //Arrange
        _service.AddContact("Spoederman", "spoederman@hotmail.com", "0478125689");
        //Act
        _service.ChangeContact(1, "Spoederman", null, null);
        var result = _service.GetContactById(1);
        //Assert
        Assert.Null(result.Email);
        Assert.Null(result.GsmNummer);
    }

    [Fact]
    public void ChangeContact_InvalidId_ShouldThrowKeyNotFoundException()
    {
        Assert.Throws<KeyNotFoundException>(() =>
            _service.ChangeContact(420, "tim", "tim@hotmail.com", "122345")
        );
    }

    [Fact]
    public void RemoveContact_ValidId_ShouldRemoveFromRepository()
    {
        //Arrange
        _service.AddContact("Spoederman", "spoederman@hotmail.com", "0478125689");
        _service.AddContact("Timmy", "Timmy@gmail.com", "");
        _service.AddContact("Tommy", "", "12345");
        //Act
        _service.RemoveContact(1);
        //Assert
        Assert.Equal(2, _service.GetContacts().Count);
    }

    [Fact]
    public void RemoveContact_InvalidId_ShouldThrowKeyNotFoundException()
    {
        Assert.Throws<KeyNotFoundException>(() => _service.RemoveContact(999));
    }

    [Fact]
    public void RemoveContact_LastContact_ShouldLeaveEmptyRepository()
    {
        //Arrange
        _service.AddContact("Spoederman", "spoederman@hotmail.com", "0478125689");
        //Act
        _service.RemoveContact(1);
        //Assert
        Assert.Empty(_service.GetContacts());
    }
}
