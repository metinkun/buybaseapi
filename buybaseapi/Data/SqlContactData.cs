using System;
using System.Collections.Generic;
using System.Linq;
using buybaseapi.Models;
using Microsoft.EntityFrameworkCore;

namespace buybaseapi.Data
{
    public class SqlContactData : IContactData
    {
        private ContactContext _contactContext;
        public SqlContactData(ContactContext contactContext) 
        {
            _contactContext = contactContext;
        }

        public Contact AddContact(Contact contact)
        {
            contact.Id = Guid.NewGuid();
            _contactContext.Contacts.Add(contact);
            _contactContext.SaveChanges();
            return contact;
        }

        public void DeleteContact(Contact contact)
        {
            _contactContext.Contacts.Remove(contact);
            _contactContext.SaveChanges();
        }

        public Contact EditContact(Contact contact)
        {
            var existingContact = _contactContext.Contacts.Find(contact.Id);
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.Firm = contact.Firm;

                _contactContext.Contacts.Update(existingContact);
                _contactContext.SaveChanges();
            }
            
            return contact;
        }

        public Contact GetContact(Guid id)
        {
            return _contactContext.Contacts.Find(id);
        }

        public List<Contact> GetContacts()
        {
            var list =  _contactContext.Contacts.Include(x=>x.Informations).ToList();
             list = _contactContext.Contacts.Include(x => x.Informations).ToList();

            return list;
        }

    }
}
