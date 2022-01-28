using System;
using System.Collections.Generic;
using System.Linq;
using buybaseapi.Data;
using buybaseapi.Models;

namespace buybaseapi.test
{
    public class ContactsFake : IContactData
    {
        readonly List<Contact> _contacts;
        public ContactsFake()
        {
            _contacts = new List<Contact>()
            {
                new Contact()
                {
                    Id = new Guid("ab2bd817-98cd-4cf3-a80a-53ea0cd9c200"),
                    FirstName = "Metin",
                    LastName = "Kun",
                    Firm = "Test firm1"
                },
                new Contact()
                {
                    Id = new Guid("815accac-fd5b-478a-a9d6-f171a2f6ae7f"),
                    FirstName = "Cetin",
                    LastName = "Kun",
                    Firm = "Test firm2"
                },
                new Contact()
                {
                    Id = new Guid("33704c4a-5b87-464c-bfb6-51971b4d18ad"),
                    FirstName = "ali",
                    LastName = "Kun",
                    Firm = "Test firm2"
                },
            };
        } 

        public Contact AddContact(Contact contact)
        {
            contact.Id = Guid.NewGuid();
            _contacts.Add(contact);
            return contact;
        }

        public void DeleteContact(Contact contact)
        {
            var existing = _contacts.First(a => a.Id == contact.Id);
            _contacts.Remove(existing);
        }

        public Contact EditContact(Contact contact)
        {
            var existingContact = _contacts.Where(a => a.Id == contact.Id)
            .FirstOrDefault();
            existingContact.FirstName = contact.FirstName;
            existingContact.LastName = contact.LastName;
            existingContact.Firm = contact.Firm;
            return existingContact;
        }

        public Contact GetContact(Guid id)
        {
            return _contacts.Where(a => a.Id == id)
            .FirstOrDefault();
        }

        public List<Contact> GetContacts()
        {
            return _contacts;
        }
    }
}
