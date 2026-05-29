namespace ContactManager.Api.Tests;

using System.Net;
using System.Net.Http.Json;
using ContactManager.Core;
using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;

public class ContactApiTests
{
    private readonly CustomWebApplicationFactory factory = new();

    [Fact]
    public async Task Post_contact_creates_a_contact()
    {
        // Given
        var client = factory.CreateClient();

        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace",
            Email = "AdaLoveLace@Gmail.com",
            GsmNummer = "123456789",
        };
        //When
        var response = await client.PostAsJsonAsync("/api/contacts", request);

        //Then
        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var created = await response.Content.ReadFromJsonAsync<CreateContactResponse>();
        Assert.NotNull(created);
        Assert.NotEqual(0, created.Id);
    }

    [Fact]
    public async Task Get_all_contacts_shows_all_contacts()
    {
        // Given
        var client = factory.CreateClient();
        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace",
            Email = "AdaLoveLace@Gmail.com",
            GsmNummer = "123456789",
        };
        await client.PostAsJsonAsync("/api/contacts", request);

        // When
        var contacts = await client.GetFromJsonAsync<List<ContactResponse>>("/api/contacts");

        // Then
        Assert.NotNull(contacts);
        Assert.NotEmpty(contacts);
    }

    [Fact]
    public async Task Get_search_by_name_filters_contacts_by_name()
    {
        // Given
        var client = factory.CreateClient();
        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace",
            Email = "AdaLoveLace@Gmail.com",
            GsmNummer = "123456789",
        };
        await client.PostAsJsonAsync("/api/contacts", request);
        var request2 = new CreateContactRequest
        {
            Name = "Buba",
            Email = "buba@Gmail.com",
            GsmNummer = "",
        };
        await client.PostAsJsonAsync("/api/contacts", request2);
        // When
        var results = await client.GetFromJsonAsync<List<ContactResponse>>(
            "/api/contacts/search?name=Buba"
        );

        // Then
        Assert.NotNull(results);
        Assert.Single(results);
        Assert.Equal("Buba", results[0].Name);

    }

    [Fact]
    public async Task Put_updates_a_contact()
    {
        // Given
        var client = factory.CreateClient();
        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace",
            Email = "AdaLoveLace@Gmail.com",
            GsmNummer = "123456789",
        };
        var updatedRequest = new UpdateContactRequest
        {
            Name = "Buba",
            Email = "ada@gmail.com",
            GsmNummer = "123456789",
        };
        var postResponse = await client.PostAsJsonAsync("/api/contacts", request);
        var created = await postResponse.Content.ReadFromJsonAsync<CreateContactResponse>();

        // When
        var response = await client.PutAsJsonAsync($"/api/contacts/{created.Id}", updatedRequest);
        // Then
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Put_returns_404_for_missing_contact()
    {
        // Given
        var client = factory.CreateClient();
        var updateRequest = new UpdateContactRequest
        {
            Name = "Test",
            Email = "",
            GsmNummer = "",
        };

        // When
        var response = await client.PutAsJsonAsync("/api/contacts/999", updateRequest);
        // Then
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Invalid_input_returns_400()
    {
        // Given
        var client = factory.CreateClient();

        var request = new CreateContactRequest
        {
            Name = "",
            Email = "AdaLoveLace@Gmail.com",
            GsmNummer = "123456789",
        };


        // When 
        var response = await client.PostAsJsonAsync("/api/contacts", request);

        // Then
        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

    }


    [Fact]
    public async Task Delete_Removes_a_contact()
    {
        // Given
        var client = factory.CreateClient();
        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace",
            Email = "AdaLoveLace@Gmail.com",
            GsmNummer = "123456789",
        };
        var postResponse = await client.PostAsJsonAsync("/api/contacts", request);
        var created = await postResponse.Content.ReadFromJsonAsync<CreateContactResponse>();
        // When
        var response = await client.DeleteAsync($"/api/contacts/{created.Id}");
        // Then
         
          Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }

    [Fact]
    public async Task Delete_contact_doesnt_appear_in_GET()
    {
        // Given
        var client = factory.CreateClient();
        var request = new CreateContactRequest
        {
            Name = "Ada Lovelace",
            Email = "AdaLoveLace@Gmail.com",
            GsmNummer = "123456789",
        };
        var postResponse = await client.PostAsJsonAsync("/api/contacts", request);
        var created = await postResponse.Content.ReadFromJsonAsync<CreateContactResponse>();

        // When
        var response = await client.DeleteAsync($"/api/contacts/{created.Id}");
        var contacts = await client.GetFromJsonAsync<List<ContactResponse>>("/api/contacts");

        // Then
        Assert.DoesNotContain(contacts, c =>c.Id == created.Id);
    }
}
