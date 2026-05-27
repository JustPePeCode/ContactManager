namespace ContactManager.Api.Tests;
using System.Net;
using System.Net.Http.Json;


public class ContactApiTests : IClassFixture<CustomWebApplicationFactory>
{
    private readonly CustomWebApplicationFactory factory;

    public ContactApiTests(CustomWebApplicationFactory factory)
    {
        this.factory = factory;
    }

    [Fact]
    public async Task Post_contact_creates_a_contact()
    {
        var client = factory.CreateClient();

        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace",
            Email = "AdaLoveLace@Gmail.com",
            GsmNummer = "123456789",
        };

        var response = await client.PostAsJsonAsync("/api/contacts", request);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<CreateContactResponse>();

        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
    }
}
