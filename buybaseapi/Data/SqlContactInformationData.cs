using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using buybaseapi.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using static buybaseapi.Models.ContactInformation;

namespace buybaseapi.Data
{
    public class SqlContactInformationData : IContactInformationData
    {
        private ContactContext _contactContext;

        public SqlContactInformationData(ContactContext contactContext) 
        {
            Console.WriteLine($"started");
            _contactContext = contactContext;
         
        }

        public ContactInformation AddContactInformation(ContactInformation contactInformation)
        {
            contactInformation.Id = Guid.NewGuid();
            _contactContext.ContactInformations.Add(contactInformation);
            _contactContext.SaveChanges();
            return contactInformation;
        }

        public void DeleteContactInformation(ContactInformation contactInformation)
        {
            _contactContext.ContactInformations.Remove(contactInformation);
            _contactContext.SaveChanges();
        }

       
        public ContactInformation GetContactInformation(Guid id)
        {
            return _contactContext.ContactInformations.Find(id);
        }


        public int GetCountOfContactsOfLocation(string location)
        {
            var count = _contactContext.ContactInformations
            .Where(o => o.Type == ContactInformationType.Location)
            .Where(o => o.Content == location)
            .Count();
            return count;
        }

    }
}
