using ContactManager.Data;
using ContactManager.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Controllers;

[ApiController]
[Route("[controller]")]
public class ContactController : ControllerBase
{
    private readonly DataContext _context;
    private readonly IConfiguration _config;

    public ContactController(DataContext context, IConfiguration config)
    {
        _context = context;
        _config = config;
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetContactByID(int id)
    {
        try
        {
            var contact = _context.tbl_contact.Where(contact => contact.id == id);
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }
    [HttpGet]
    public async Task<ActionResult<List<TBLContact>>> GetAllContactList()
    {
        List<TBLContact> objTAllContactList = _context.tbl_contact.OrderByDescending(i => i.id).ToList();
        return Ok(objTAllContactList);
    }

    [HttpPost]
    public async Task<ActionResult<TBLContact>> CreateContact(TBLContact contact)

    {
        // Validate the input
        if ((string.IsNullOrEmpty(contact.salutation) && contact.salutation.Length < 3) ||
            (string.IsNullOrEmpty(contact.firstname) && contact.firstname.Length < 3) ||
            (string.IsNullOrEmpty(contact.lastname) && contact.lastname.Length < 3) && (string.IsNullOrEmpty(contact.email)))
        {
            return BadRequest("Invalid input");
        }

        // Set the display name if it is empty
        if (string.IsNullOrEmpty(contact.displayname))
        {
            contact.displayname = contact.salutation + " " + contact.firstname + " " + contact.lastname;
        }

        // Set the creation timestamp
        contact.creationtimestamp = DateTime.UtcNow;

        // Add the contact to the list
        // _contacts.Add(contact);
        await _context.tbl_contact.AddAsync(contact);
        await _context.SaveChangesAsync();

        return Ok(contact);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult<TBLContact>> UpdateContact(int id, TBLContact contact)
    // public IActionResult UpdateContact(int id, Contact contact)
    {
        // Find the contact with the given ID
        var existingContact = _context.tbl_contact.FirstOrDefault(c => c.id == id);
        if (existingContact == null)
        {
            return NotFound();
        }

        // Validate the input
        if ((string.IsNullOrEmpty(contact.salutation) && contact.salutation.Length < 3) ||
              (string.IsNullOrEmpty(contact.firstname) && contact.firstname.Length < 3) ||
              (string.IsNullOrEmpty(contact.lastname) && contact.lastname.Length < 3) && (string.IsNullOrEmpty(contact.email)))
        {
            return BadRequest("Invalid input");
        }
        // Update the contact
        existingContact.salutation = contact.salutation;
        existingContact.firstname = contact.firstname;
        existingContact.lastname = contact.lastname;
        existingContact.displayname = string.IsNullOrEmpty(contact.displayname) ?
            contact.salutation + " " + contact.firstname + " " + contact.lastname : contact.displayname;
        existingContact.birthdate = contact.birthdate;
        existingContact.email = contact.email;
        existingContact.phonenumber = contact.phonenumber;

        // Set the last change timestamp
        existingContact.lastchangetimestamp = DateTime.UtcNow;
        // existingContact.lastchangetimestamp = contact.lastchangetimestamp.ToUniversalTime();
        _context.tbl_contact.Update(existingContact);
        await _context.SaveChangesAsync();
        return Ok(existingContact);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteContact(int id)
    {
        // Find the contact with the given ID
        var contact = await _context.tbl_contact.FindAsync(id);
        if (contact == null)
        {
            return NotFound();
        }

        // Remove the contact from the list
        _context.tbl_contact.Remove(contact);
        await _context.SaveChangesAsync();

        return Ok();
    }


}


