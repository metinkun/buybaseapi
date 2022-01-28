using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using buybaseapi.Models;

namespace buybaseapi.Data
{
    public interface IContactData
    {
        List<Contact> GetContacts();
        
        Contact GetContact(Guid id);
        Contact AddContact(Contact contact);
        Contact EditContact(Contact contact);
        void DeleteContact(Contact contact);

    }
}
