using ContactManager.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Services;

[ApiController]
[Route("api/contacts")]
public class ContactController : ControllerBase
{
    public ContactController(ContactService service)
    {
        _service = service;
    }

    private ContactService _service;

    [HttpGet]
    public IActionResult GetAll()
    {
        var contacts = _service
            .GetContacts()
            .Select(c => new ContactResponse
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email ?? "",
                GsmNummer = c.GsmNummer ?? "",
            });
        return Ok(contacts);
    }

    // GET/contacts

    [HttpGet("search")]
    public IActionResult Search(string name)
    {
        var result = _service
            .SearchContact(name)
            .Select(c => new ContactResponse
            {
                Id = c.Id,
                Name = c.Name,
                Email = c.Email ?? "",
                GsmNummer = c.GsmNummer ?? "",
            });

        return Ok(result);
    }

    //GET /contacts/search?name=Mark
    [HttpPost]
    public IActionResult Add([FromBody] CreateContactRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required");

        var result = _service.AddContact(request.Name, request.Email, request.GsmNummer);
        return StatusCode(201, new CreateContactResponse { Id = result });
    }

    //POST /contacts
    [HttpPut("{id}")]
    public IActionResult Update(int id, [FromBody] UpdateContactRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return BadRequest("Name is required");

        if (!_service.IdExists(id))
            return NotFound();

        _service.ChangeContact(id, request.Name, request.Email, request.GsmNummer);
        return Ok();
    }

    //PUT /contacts/{id}
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        if (!_service.IdExists(id))
        {
            return NotFound();
        }
        _service.RemoveContact(id);
        return NoContent();
    }
    //DELETE /contacts/{id}
}
