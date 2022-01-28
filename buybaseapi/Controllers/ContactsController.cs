using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buybaseapi.ContactData;
using buybaseapi.Models;

namespace buybaseapi.Controllers
{
    
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private IContactData _contactData;

        public ContactsController(IContactData contactData)
        {
            _contactData = contactData;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetContacts()
        {
            return Ok(_contactData.GetContacts());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetContact(Guid id)
        {
            var contact = _contactData.GetContact(id);
            if (contact != null)
                return Ok(contact);
            return NotFound();
        }

        

        [HttpPost]
        [Route("api/[controller]")]
        public IActionResult AddContacts(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _contactData.AddContact(contact);
            return CreatedAtAction("AddContacts", new { id = contact.Id }, contact);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public IActionResult DeleteContact(Guid id)
        {
            var contact = _contactData.GetContact(id);
            if (contact != null)
            {
                _contactData.DeleteContact(contact);
                return Ok();
            }
            return NotFound();
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]
        public IActionResult EditContact(Guid id , Contact newContact)
        {
            var contact = _contactData.GetContact(id);
            if (contact != null)
            {
                newContact.Id = id;
                _contactData.EditContact(newContact);
                return Ok(newContact);
            }
            return NotFound();
        }
    }
}
